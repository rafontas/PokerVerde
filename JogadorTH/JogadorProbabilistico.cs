using Comum.Interfaces;
using Comum.Interfaces.AnaliseProbabilidade;
using JogadorTH.Inteligencia.Probabilidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public class JogadorProbabilistico : JogadorBase
    {
        public IRetornaProbabilidade RetornaProbabilidade { get; set; }

        public JogadorProbabilistico(IConfiguracaoPoker Config, IAcaoProbabilidade acaoProbabilidade, IRetornaProbabilidade RetornaProbabilidade, uint valorStackInicial = 200, IAcoesDecisao inteligencia = null) : base(valorStackInicial)
        {

            if (inteligencia == null)
            {
                inteligencia = new Inteligencia.InteligenciaProb(acaoProbabilidade, RetornaProbabilidade);
            }
            inteligencia.JogadorStack = this.JogadorStack;
            inteligencia.Corrida = this.Corrida;
            this.Mente.Add(inteligencia);
            this.ConfiguracaoPoker = Config;

        }
    }
}
