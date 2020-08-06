using Comum.Excecoes;
using Comum.Interfaces;
using Enuns;
using JogadorTH.Inteligencia;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public class DummyJogadorTHB : JogadorBase
    {
        public DummyJogadorTHB(ConfiguracaoTHBonus Config, uint valorStackInicial = 200, IAcoesDecisao inteligencia = null) : base(Config, valorStackInicial)
        {
            this.config = Config;

            if (inteligencia == null)
            {
                inteligencia = new DummyInteligencia();
            }

            inteligencia.JogadorStack = this.JogadorStack;
            inteligencia.Corrida = this.Corrida;
            this.Mente.Add(inteligencia);
        }
    }
}
