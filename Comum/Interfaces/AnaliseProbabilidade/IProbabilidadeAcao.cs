using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface IProbabilidadeAcao
    {
        int Id { get; }
        float CallFlop { get; }
        float CallCheckTurn { get; }
        float CallRaiseTurn { get; }
        float CallCheckRiver { get; }
        float CallRaiseRiver { get; }
    }
}
