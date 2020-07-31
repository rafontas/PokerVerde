using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH.Inteligencia.Probabilidade
{
    internal class AcaoProbabilidade
    {
        internal float probMinimaChamarFlop { get; } = 50;
        internal float probMinAumentaNoFlop { get; } = 51;
        internal float probMinAumentaNoTurn { get; } = 51;
    }
}
