using DealerTH.Probabilidade;
using Modelo;
using PokerDAO;
using System;
using System.Collections.Generic;

namespace MaoTH
{
    public class AnaliseConvergenciaProbabilidadePorJogosSimulados
    {
        public int NumeroCartasAleatorias { get; set; }
        public int LimiteMaximoJogosSimulados { get; set; }
        public int PassoSimulacoes { get; set; }
        public int QuantidadeInicialJogosSimulados { get; set; }
        private Random random { get; set; } = new Random();

        private HashSet<string> MaosAleatoriasGeradas { get; set; } = new HashSet<string>();

        private Carta [] GetNovaMaoIneditaSuited()
            => this.GetNovaMaoInedita(Enuns.Naipe.Espadas, Enuns.Naipe.Espadas);

        private Carta[] GetNovaMaoIneditaOff()
            => this.GetNovaMaoInedita(Enuns.Naipe.Copas, Enuns.Naipe.Espadas);

        private Carta GetCartaAleatoria(Enuns.Naipe naipe = Enuns.Naipe.Copas, Carta DiferenteDe = null)
        {
            int NumUm = this.random.Next(2, 14);

            if (DiferenteDe == null)
                return new Carta((uint)NumUm, naipe);

            //evita carta de mesmo naipe e número
            while (naipe == DiferenteDe.Naipe && NumUm == DiferenteDe.Numero)
            {
                NumUm = this.random.Next(2, 14);
                naipe = Carta.GetNaipeAleatorio();
            }

            return new Carta((uint)NumUm, naipe);
        }

        private Carta [] GetNovaMaoInedita(Enuns.Naipe naipeCarta1, Enuns.Naipe naipeCarta2)
        {
            if (this.MaosAleatoriasGeradas.Count >= 500) throw new Exception("Limite de mãos atingido");

            Carta carta1, carta2;

            string novaMao = "";

            do
            {
                carta1 = GetCartaAleatoria(naipeCarta1);
                carta2 = GetCartaAleatoria(naipeCarta2, carta1);

                if (carta1.Numero < carta2.Numero)
                {
                    Carta carta3 = carta1;
                    carta1 = carta2;
                    carta2 = carta3;
                }

                novaMao = carta1.ToString() + " " + carta2.ToString();

            } while (MaosAleatoriasGeradas.Contains(novaMao));

            this.MaosAleatoriasGeradas.Add(novaMao);

            return new Carta[] { carta1, carta2 };
        }
        
        public void AnaliseConvergenciaMaoQuantidadeJogos()
        {
            IList<AnaliseConvergencia> analises = new List<AnaliseConvergencia>();

            for (int numCartasAleatorias = 0; numCartasAleatorias < this.NumeroCartasAleatorias; numCartasAleatorias++)
            {
                Carta[] maoOff = this.GetNovaMaoIneditaOff();

                for (int numJogosJogados = this.QuantidadeInicialJogosSimulados; numJogosJogados <= this.LimiteMaximoJogosSimulados; numJogosJogados += this.PassoSimulacoes)
                {
                    AnaliseConvergencia analise = new AnaliseConvergencia()
                    {
                        NumeroDeCartas = maoOff.Length,
                        Cartas = maoOff
                    };

                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    
                    analise.QuantidadeDeJogosExecutados = numJogosJogados;
                    analise.Probabilidade = AvaliaProbabilidadeMao.GetPorcentagemVitoria(maoOff, (uint)numJogosJogados);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    analise.TempoDeExecucao = new TimeSpan(elapsedMs * TimeSpan.TicksPerMillisecond);
                    analises.Add(analise);
                }
            }

            // Persiste a analise de convergência
            AnaliseConvergencia.Persiste(analises);
        }
    }
}
