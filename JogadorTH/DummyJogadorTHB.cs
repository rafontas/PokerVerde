using Comum.Excecoes;
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
        public DummyJogadorTHB(ConfiguracaoTHBonus Config, uint valorStackInicial = 200) : base(Config, valorStackInicial)
        {
            this.config = Config;
            this.Mente.Add(new DummyInteligencia());
        }
    }
}
