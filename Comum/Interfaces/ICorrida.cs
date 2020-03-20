using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface ICorrida
    {
        uint QtdPartidasJogadas { get; }
        uint ValorInicial { get; }
        uint ValorkAtual { get; }
        uint ValorGanho { get; }
        uint ValorPerdido { get; }
        uint ValorDeParada { get; }
    }
}
