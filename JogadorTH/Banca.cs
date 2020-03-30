using JogadorTH.Acoes;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public class Banca : JogadorBase
    {
        public Banca(ConfiguracaoTHBonus Config, uint valorStackInicial = 500000) : base(Config, valorStackInicial)
        {
            this.Mente.Add(new AcaoDecisoriaBanca());
        }

    }
}
