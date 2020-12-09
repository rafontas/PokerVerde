using Comum.Interfaces.AnaliseProbabilidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes.Poker.AnaliseProbabilidade
{
    public class AcaoProbabilidade : IAcaoProbabilidade
    {
        public float probMinimaChamarFlop { get; set; }
        public float probMinAumentaNoFlop { get; set; }
        public float probMinAumentaNoTurn { get; set; }
    }
}
