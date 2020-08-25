using PokerDAO.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaoTH.Probabilidade.ProbMaoInicial
{
    public class ProbabilidadeMaoInicial : IProbabilidadeMaoInicial
    {
        public uint NumCarta1 { get; set; }

        public uint NumCarta2 { get; set; }

        public char OffOrSuited { get; set; }

        public float Probabilidade { get; set; }

        public uint QuantidadesJogosSimulados { get; set; }
    }
}
