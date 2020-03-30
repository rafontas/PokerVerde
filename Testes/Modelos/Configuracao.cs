using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testes.Modelos
{
    public static class Configuracao
    {
        public static ConfiguracaoTHBonus configPadrao
        {
            get
            {
                return new ConfiguracaoTHBonus()
                {
                    Ant = 5,
                    Flop = 10,
                    Turn = 5,
                    River = 5,
                };
            }
        }

    }
}
