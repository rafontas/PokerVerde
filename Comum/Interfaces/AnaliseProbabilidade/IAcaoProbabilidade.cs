using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface IAcaoProbabilidade
    {
        float probMinimaChamarFlop { get; set; }
        float probMinAumentaNoFlop { get; set; }
        float probMinAumentaNoTurn { get; set; }
    }
}
