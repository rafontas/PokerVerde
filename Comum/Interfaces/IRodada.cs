using System;
using System.Collections.Generic;
using System.Text;
using Enuns;

namespace Comum.Interfaces
{
    public interface IRodada
    {
        TipoRodada TipoRodada { get; }
        IList<IAcao> Acoes { get; }
    }
}
