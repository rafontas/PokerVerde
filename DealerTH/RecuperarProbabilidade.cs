using Comum.Classes.Poker.AnaliseProbabilidade;
using Comum.Interfaces;
using Comum.Interfaces.AnaliseProbabilidade;
using DealerTH.Probabilidade;
using Modelo;
using PokerDAO.Contextos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MaoTH
{
    public class RecuperarProbabilidade : IRetornaProbabilidade
    {
        public static string ResumoCache { 
            get => Resumos.Last();
            set => Resumos.Add(value);
        }

        private static IList<string> Resumos { get; set; } = new List<string>();
        private static MedidorEficaciaCacheDeProbabilidade ModificadorProb { get; set; } 
        
        public uint NumeroRodadas { get; set; }

        private bool MedirEficaciaCacheProbabilidade { get; set; } = false;

        //private Dictionary<string, float> CacheProbabilidades = new Dictionary<string, float>();
        private IDictionary<string, IDictionary<string, float>> CacheProbabilidades 
            = new Dictionary<string, IDictionary<string, float>>();

        private MedidorEficaciaCacheDeProbabilidade MedidorEficaciaCacheProbabilidade { get; set; }

        public RecuperarProbabilidade(bool medirEficaciaCacheProbabilidade = true)
        {
            this.CarregaProbabilidadesCalculadas();
            this.MedirEficaciaCacheProbabilidade = medirEficaciaCacheProbabilidade;
            this.MedidorEficaciaCacheProbabilidade = new MedidorEficaciaCacheDeProbabilidade();
        }

        private void CarregaProbabilidadesCalculadas()
        {
            this.CacheProbabilidades = MaoProbabilidade.GetPocketHands();

            foreach(var item in this.CacheProbabilidades)
            {
                IList<IMaoProbabilidade> listaProbabilidades = MaoProbabilidadeContexto.GetHandLike(item.Key);

                if (item.Key == "20 K0")
                {
                    int i = 0;
                }

                foreach (var prob in listaProbabilidades)
                {
                    item.Value.Add(prob.HandTokenizada, prob.ProbabilidadeVitoria);
                }
            }
        }

        /// <summary>
        /// Carreta a probabilidade de uma mão vencer, que já está no banco de dados.
        /// </summary>
        /// <param name="maoAvaliada">Cartas da mão.</param>
        /// <returns>Probabilidade de ganhar. Ex: 50.10</returns>    
        public float GetProbabilidadeVitoria(Carta[] mao) => this.GetProbabilidadeVitoria(mao, null);

        private int contagemMaoDuasCartas { get; set; } = 0;

        public float GetProbabilidadeVitoria(Carta[] mao, Carta[] mesa = null)
        {
            Carta[] maoCompleta = new Carta[mao.Length + (mesa?.Length ?? 0)];

            mao.CopyTo(maoCompleta, 0);
            if (mesa != null) mesa.CopyTo(maoCompleta, mao.Length);

            IMaoProbabilidade mao_procurada = new MaoProbabilidade(mao, mesa);
            string token_mao = mao_procurada.ToMaoTokenizada();

            float minhaProbabilidade;

            if (this.CacheProbabilidades[mao_procurada.PocketHandTokenizada].TryGetValue(token_mao, out minhaProbabilidade))
            {
                if (maoCompleta.Length == 2 && ++contagemMaoDuasCartas >= 1200)
                {
                    this.MedidorEficaciaCacheProbabilidade.ReiniciaContagem = true;
                    contagemMaoDuasCartas = 0;

                }

                if (this.MedirEficaciaCacheProbabilidade)
                {
                    RecuperarProbabilidade.ResumoCache = this.MedidorEficaciaCacheProbabilidade.GetResumoCache();
                    this.MedidorEficaciaCacheProbabilidade.AddCacheHit();
                }

                return minhaProbabilidade;
            }

            minhaProbabilidade = (float) AvaliaProbabilidadeMao.GetRecalculaVitoriaParalelo(mao, mesa, null, 50000);
            mao_procurada.ProbabilidadeVitoria = minhaProbabilidade;

            this.CacheProbabilidades[mao_procurada.PocketHandTokenizada].Add(mao_procurada.HandTokenizada, mao_procurada.ProbabilidadeVitoria);
            MaoProbabilidadeContexto.Persiste(mao_procurada);
            if (this.MedirEficaciaCacheProbabilidade) this.MedidorEficaciaCacheProbabilidade.AddCacheMiss();

            return minhaProbabilidade; ;
        }

        internal class MedidorEficaciaCacheDeProbabilidade
        {
            internal uint QtdProbabilidadesPersistidasInicio { get; private set; }
            internal uint QtdProbabilidadesPersistidasFim { get; private set; }
            internal uint NumeroCacheHit { get; private set; } = 0;
            internal uint NumeroCacheMiss { get; private set; } = 0;
            internal uint NumeroTotalConsultas { get; private set; } = 0;
            internal uint NumeroConsultasAhAvaliar { get; private set; }
            internal string NomeArquivoSalvar { get; private set; } = "EficaciaCacheProbabilidade.txt";
            private bool ImprimirCabecarioTela { get; set; } = false;
            internal bool ModoVerboso { get; set; } = false;
            internal bool ReiniciaContagem { get; set; } = false;
            internal TimeSpan UltimoTempo { get; set; }
            internal Stopwatch Contador { get; set; } = new Stopwatch();

            internal MedidorEficaciaCacheDeProbabilidade(uint QuantidadeConsultasAvaliar = 0) { 
                this.QtdProbabilidadesPersistidasInicio = (uint) MaoProbabilidadeContexto.GetQuantidadeItensPersistidos();
                this.NumeroConsultasAhAvaliar = QuantidadeConsultasAvaliar;
                ImprimirCabecarioTela = true;
            }

            internal void AddCacheMiss() {
                this.NumeroCacheMiss++;
                this.AddTotalConsultas();
            }
            internal void AddCacheHit() {
                this.NumeroCacheHit++;
                this.AddTotalConsultas();
            }
            private void AddTotalConsultas() 
            {
                this.NumeroTotalConsultas++;

                if ((this.NumeroConsultasAhAvaliar > 0 && this.NumeroTotalConsultas >= this.NumeroConsultasAhAvaliar)
                    || this.ReiniciaContagem == true)
                {
                    this.ReiniciaContagem = false;
                    this.AppendFinalArquivo();
                    RecuperarProbabilidade.ResumoCache = this.GetResumoCache();
                    this.ReiniciarContagem();
                }
                if (this.NumeroTotalConsultas == 1)
                {
                    this.Contador.Reset();
                    this.Contador.Start();
                }
            }
            private void ReiniciarContagem()
            {
                this.QtdProbabilidadesPersistidasInicio = (uint)MaoProbabilidadeContexto.GetQuantidadeItensPersistidos();
                this.QtdProbabilidadesPersistidasFim = 0;
                this.NumeroCacheHit = 0;
                this.NumeroCacheMiss = 0;
                this.NumeroTotalConsultas = 0;
            }
            internal void AppendFinalArquivo()
            {
                this.QtdProbabilidadesPersistidasFim = (uint) MaoProbabilidadeContexto.GetQuantidadeItensPersistidos();
                File.AppendAllText(this.NomeArquivoSalvar, this.ToString());
            }

            public override string ToString()
            {
                string cabecarioMedidorCache = Environment.NewLine + "Total\tHits\tMiss\t\tQtdProbPersistidasInicio\tQtdProbPersistidasFim\tDiferença";
                string dadosMedidor = string.Empty;
                StringBuilder strBuilder = new StringBuilder(dadosMedidor);

                strBuilder.AppendLine();
                strBuilder.AppendFormat("{0}\t{1}\t{2}", this.NumeroTotalConsultas, this.NumeroCacheHit, this.NumeroCacheMiss);
                strBuilder.AppendFormat("\t-\t{0}\t\t\t\t{1}\t\t\t{2}",
                        this.QtdProbabilidadesPersistidasInicio,
                        this.QtdProbabilidadesPersistidasFim,
                        this.QtdProbabilidadesPersistidasFim - this.QtdProbabilidadesPersistidasInicio);
                strBuilder.Append(this.GetTempoExecucao());

                if (this.ModoVerboso)
                {
                    if (this.ImprimirCabecarioTela)
                    {
                        Console.WriteLine(cabecarioMedidorCache);
                        this.ImprimirCabecarioTela = false;
                    }
                    Console.WriteLine(strBuilder.ToString());
                }

                RecuperarProbabilidade.ModificadorProb = this;

                return strBuilder.ToString();
            }
            public string GetResumoCache()
            {
                return string.Format(" - Cache: {0}/{1}, {2} Ef: {3}%", this.NumeroTotalConsultas.ToString("0,00"), this.NumeroCacheHit.ToString("0,00"), this.NumeroCacheMiss.ToString("0,00"), (((float)this.NumeroCacheHit * 100) / (float)this.NumeroTotalConsultas).ToString("0.0"));
            }
        
            private string GetTempoExecucao() 
            {
                this.Contador.Stop();
                this.UltimoTempo = this.Contador.Elapsed;

                return string.Format(" Tempo Exec: {0:D2}:{1:D2}", this.UltimoTempo.Minutes, this.UltimoTempo.Seconds);
            }
        }
    }
}
