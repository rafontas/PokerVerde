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
    public class CarregaProbabilidade : IRetornaProbabilidade
    {

        public uint NumeroRodadas { get; set; }

        private static Dictionary<IProbabilidadeMaoInicial, float> ProbabilidadeMaoInicial = new Dictionary<IProbabilidadeMaoInicial, float>();
        private KeyValuePair<IProbabilidadeMaoInicial, float> UltimaProbabilidadeProcurada;
        private bool SearchCache(IMaoBasica maoBasicaProcurada, out float minhaProbabilidade)
        {
            minhaProbabilidade = 0.0f;

            if (!this.UltimaProbabilidadeProcurada.Equals(new KeyValuePair<IProbabilidadeMaoInicial, float>()))
            {
                if (this.UltimaProbabilidadeProcurada.Key.NumCarta1 == maoBasicaProcurada.NumCarta1
                    && this.UltimaProbabilidadeProcurada.Key.NumCarta2 == maoBasicaProcurada.NumCarta2
                    && this.UltimaProbabilidadeProcurada.Key.OffOrSuited == maoBasicaProcurada.OffOrSuited)
                {
                    minhaProbabilidade = this.UltimaProbabilidadeProcurada.Value;
                    return true;
                }
            }

            return false;
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

            if (this.SearchCache(maoBasicaProcurada, out minhaProbabilidade)) return minhaProbabilidade;

            IProbabilidadeMaoInicial prob = ProbabilidadeMaoInicialContext.GetItem(maoBasicaProcurada);
            this.UltimaProbabilidadeProcurada = new KeyValuePair<IProbabilidadeMaoInicial, float>(prob, prob.ProbabilidadeVitoria);

            return this.UltimaProbabilidadeProcurada.Value;
        }

        public float GetProbabilidadeVitoria(Carta[] mao, Carta[] mesa) => AvaliaProbabilidadeMao.GetRecalculaVitoria(mao, mesa, 200000);
    }
}
