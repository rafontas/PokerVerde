using System;
using System.Collections.Generic;
using System.Text;
using Enuns;
using Modelo;

namespace Comum.Interfaces
{
    public interface IRodada
    {
        TipoRodada TipoRodada { get; }
        Carta[] CartasMesa { get; set; } 
        uint ValorPotePreRodada { get; set; }
    }
}