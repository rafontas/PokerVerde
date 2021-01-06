using Comum.Interfaces.AnaliseProbabilidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes.Poker.AnaliseProbabilidade
{
    public class AcaoProbabilidade : IAcaoProbabilidade
    {
        public int id { get; set; }
        public float probabilidadeMinicaSeeFlop { get; set; }
        public float probabilidadeMinimaRaisePreTurn { get; set; }
        public float probabilidadeMinimaRaisePreRiver { get; set; }

        public bool Equals(IAcaoProbabilidade other)
        {
            if (this.probabilidadeMinicaSeeFlop == other.probabilidadeMinicaSeeFlop &&
                this.probabilidadeMinimaRaisePreRiver == other.probabilidadeMinimaRaisePreRiver &&
                this.probabilidadeMinimaRaisePreTurn == other.probabilidadeMinimaRaisePreTurn)
                return true;

            return false;
        }
    }
}
