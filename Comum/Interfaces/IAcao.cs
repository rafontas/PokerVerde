using System;
using System.Collections.Generic;
using System.Text;
using Modelo;
using Enuns;

namespace Comum.Interfaces
{
    public interface IAcao
    {
        uint Sequence { get; }
        TipoAcao Tipo { get; }
        IJogador Realizador { get; }
        uint Valor { get; }
    }
}
