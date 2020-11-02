using Comum.Classes;
using Comum.Interfaces;
using DealerTH.Probabilidade;
using Modelo;
using PokerDAO.Contextos;
using System.Collections.Generic;

namespace MaoTH.DAO
{
    public class CalculadoraProbabilidadeMaosInicial
    {
        public uint QuantidadeJogosPorSimulacao { get; private set; }

        public IList<IProbabilidadeMaoInicial> ProbabilidadesMaos { get; set; }
        
        public bool HaProbabilidadeSalva() {
            throw new System.NotImplementedException("");
        }

        public IList<IProbabilidadeMaoInicial> Carregar() => 
            ProbabilidadeMaoInicialContext.GetMaosProbabilidadesIniciais((int)this.QuantidadeJogosPorSimulacao);

        public void Gerar() 
        {
            this.ProbabilidadesMaos = new List<IProbabilidadeMaoInicial>();
            this.GerarMaosOff();  
            this.GerarMaosSuited();  
        }
        
        public void Salvar() 
        {
            ProbabilidadeMaoInicialContext.Persiste(this.ProbabilidadesMaos);
        }

        private void GerarMaosOff() 
        {
            for (uint i = 2; i <= 14; i++)
            {
                for (uint j = i; j <= 14; j++)
                {

                    IProbabilidadeMaoInicial probMao = new ProbabilidadeMaoInicial()
                    {
                        NumCarta1 = i,
                        NumCarta2 = j,
                        OffOrSuited = 'O',
                        QuantidadesJogosSimulados = this.QuantidadeJogosPorSimulacao
                    };

                    if (ProbabilidadeMaoInicialContext.JaExisteProbabilidadeCadastrada(probMao)) continue;

                    Carta[] maoOff = new Carta[] {
                        new Carta(i, Enuns.Naipe.Copas),
                        new Carta(j, Enuns.Naipe.Espadas)
                    };

                    probMao.ProbabilidadeVitoria = AvaliaProbabilidadeMao.GetPorcentagemVitoria(maoOff, this.QuantidadeJogosPorSimulacao);

                    ProbabilidadeMaoInicialContext.Persiste(probMao);
                }
            }
        }

        private void GerarMaosSuited() 
        {
            for (uint i = 2; i < 14; i++)
            {
                for (uint j = i+1; j <= 14; j++)
                {

                    IProbabilidadeMaoInicial probMao = new ProbabilidadeMaoInicial()
                    {
                        NumCarta1 = i,
                        NumCarta2 = j,
                        OffOrSuited = 'S',
                        QuantidadesJogosSimulados = this.QuantidadeJogosPorSimulacao
                    };

                    if (ProbabilidadeMaoInicialContext.JaExisteProbabilidadeCadastrada(probMao)) continue;

                    Carta[] maoSuited = new Carta[] {
                        new Carta(i, Enuns.Naipe.Copas),
                        new Carta(j, Enuns.Naipe.Copas)
                    };

                    string chaveMaoSuited = maoSuited[0].ToString() + " " + maoSuited[1].ToString();

                    probMao.ProbabilidadeVitoria = AvaliaProbabilidadeMao.GetPorcentagemVitoria(maoSuited, this.QuantidadeJogosPorSimulacao);

                    ProbabilidadeMaoInicialContext.Persiste(probMao);
                }
            }
        }
        
        public CalculadoraProbabilidadeMaosInicial(uint QuantidadeJogosSimulacao) 
        {
            this.QuantidadeJogosPorSimulacao = QuantidadeJogosSimulacao;
        }
    }
}
