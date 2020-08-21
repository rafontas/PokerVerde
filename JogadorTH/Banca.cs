using Comum.Interfaces;
using JogadorTH.Inteligencia;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public class Banca : JogadorBase
    {
        public Banca(IConfiguracaoPoker Config, uint valorStackInicial = 500000) : base(valorStackInicial)
        {
            IAcoesDecisao inteligencia = new InteligenciaBanca();
            inteligencia.JogadorStack = this.JogadorStack;
            this.Mente.Add(inteligencia);
            this.ConfiguracaoPoker = Config;
        }

    }
}
