using DealerTH.Probabilidade;
using MaoTH.Probabilidade.ProbMaoInicial;
using Modelo;
using PokerDAO.Contextos;
using PokerDAO.Interface;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MaoTH.DAO
{
    public class CalculadoraProbabilidadeMaosInicial
    {
        public uint QuantidadeJogosPorSimulacao { get; private set; }

        public IList<IProbabilidadeMaoInicial> ProbabilidadesMaos { get; set; }
        
        public bool HaProbabilidadeSalva() {
            throw new System.NotImplementedException("");
        }
        
        public void Carregar() { }

        public void Gerar() 
        {
            this.ProbabilidadesMaos = new List<IProbabilidadeMaoInicial>();
            this.GerarMaosOff();  
            this.GerarMaosSuited();  
        }
        
        public void Salvar() 
        {
            ProbabilidadeApenasDuasCartasContext.Persiste(this.ProbabilidadesMaos);
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

                    if (ProbabilidadeApenasDuasCartasContext.JaExisteProbabilidadeCadastrada(probMao)) continue;

                    Carta[] maoOff = new Carta[] {
                        new Carta(i, Enuns.Naipe.Copas),
                        new Carta(j, Enuns.Naipe.Espadas)
                    };

                    probMao.Probabilidade = AvaliaProbabilidadeMao.GetPorcentagemVitoria(maoOff, this.QuantidadeJogosPorSimulacao);

                    ProbabilidadeApenasDuasCartasContext.Persiste(probMao);
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

                    if (ProbabilidadeApenasDuasCartasContext.JaExisteProbabilidadeCadastrada(probMao)) continue;

                    Carta[] maoSuited = new Carta[] {
                        new Carta(i, Enuns.Naipe.Copas),
                        new Carta(j, Enuns.Naipe.Copas)
                    };

                    string chaveMaoSuited = maoSuited[0].ToString() + " " + maoSuited[1].ToString();

                    probMao.Probabilidade = AvaliaProbabilidadeMao.GetPorcentagemVitoria(maoSuited, this.QuantidadeJogosPorSimulacao);

                    ProbabilidadeApenasDuasCartasContext.Persiste(probMao);
                }
            }
        }
        
        public CalculadoraProbabilidadeMaosInicial(uint QuantidadeJogosSimulacao) 
        {
            this.QuantidadeJogosPorSimulacao = QuantidadeJogosSimulacao;
        }
    }
}
