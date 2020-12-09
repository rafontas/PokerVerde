using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH.Inteligencia.Probabilidade
{
    public class AcaoProbabilidade
    {
        public float probMinimaChamarFlop { get; } = 50;
        public float probMinAumentaNoFlop { get; } = 51;
        public float probMinAumentaNoTurn { get; } = 51;
    }
}
