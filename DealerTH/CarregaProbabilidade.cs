using Comum.Classes.Poker.AnaliseProbabilidade;
using Comum.Interfaces;
using Comum.Interfaces.AnaliseProbabilidade;
using DealerTH.Probabilidade;
using Modelo;
using PokerDAO.Contextos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaoTH
{
    public class RecuperaProbabilidade : IRetornaProbabilidade
    {
        private AvaliaProbabilidadeMao AvaliaProbabilidadeMao;

        public uint NumeroRodadas { get; set; }

        private Dictionary<IMaoBasica, float> ProbabilidadeMaoInicial = new Dictionary<IMaoBasica, float>();

        public RecuperaProbabilidade()
        {
            this.CarregaProbabilidadeMaosIniciais();
            //this.AvaliaProbabilidadeMao = new AvaliaProbabilidadeMao();
        }

        private void CarregaProbabilidadeMaosIniciais ()
        {
            IMaoBasica mao;

            foreach (var p in ProbabilidadeMaoInicialContext.GetMaosProbabilidadesIniciais()) 
            {
                mao = new MaoBasica()
                {
                    NumCarta1 = p.NumCarta1,
                    NumCarta2 = p.NumCarta2,
                    OffOrSuited = p.OffOrSuited
                };

                this.ProbabilidadeMaoInicial.Add(mao, p.ProbabilidadeVitoria);
            }
        }

        /// <summary>
        /// Carreta a probabilidade de uma mão vencer, que já está no banco de dados.
        /// </summary>
        /// <param name="maoAvaliada">Cartas da mão.</param>
        /// <returns>Probabilidade de ganhar. Ex: 50.10</returns>    
        public float GetProbabilidadeVitoria(Carta[] mao)
        {
            IMaoBasica maoBasicaProcurada = MaoBasica.ToMao(mao[0], mao[1]);
            float minhaProbabilidade = 0.0f;

            if (this.ProbabilidadeMaoInicial.TryGetValue(maoBasicaProcurada, out minhaProbabilidade)) 
                return minhaProbabilidade;

            IProbabilidadeMaoInicial prob = ProbabilidadeMaoInicialContext.GetItem(maoBasicaProcurada);
            this.ProbabilidadeMaoInicial.Add(maoBasicaProcurada, prob.ProbabilidadeVitoria);

            return prob.ProbabilidadeVitoria;
        }

        public float GetProbabilidadeVitoria(Carta[] mao, Carta[] mesa)
        {
            float probGanhar = 0.0f;

            if (mesa == null || mesa.Length <= 0)
            {
                probGanhar = this.GetProbabilidadeVitoria(mao);
            }
            else
            {
                probGanhar = AvaliaProbabilidadeMao.GetRecalculaVitoria(mao, mesa, 100000);
            }

            return probGanhar;
        }
    }
}
