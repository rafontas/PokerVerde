using Comum.Excecoes;
using Enuns;
using JogadorTH.Acoes;
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
            this.stack = valorStackInicial;
            this.Mente.Add(new DummyInteligencia());
        }
    }
}
