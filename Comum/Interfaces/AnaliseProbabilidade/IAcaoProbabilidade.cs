using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface IAcaoProbabilidade
    {
        int id { get; set; }
        float probabilidadeMinicaSeeFlop { get; set; }
        float probabilidadeMinimaRaisePreTurn { get; set; }
        float probabilidadeMinimaRaisePreRiver { get; set; }
    }
}
