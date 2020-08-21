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
        public DummyJogadorTHB(IConfiguracaoPoker Config, uint valorStackInicial = 200, IAcoesDecisao inteligencia = null) : base(valorStackInicial)
        {

            if (inteligencia == null)
            {
                inteligencia = new DummyInteligencia();
            }

            inteligencia.JogadorStack = this.JogadorStack;
            inteligencia.Corrida = this.Corrida;
            this.Mente.Add(inteligencia);
            this.ConfiguracaoPoker = Config;
        }
    }
}
