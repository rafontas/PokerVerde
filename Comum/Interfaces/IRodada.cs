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
        Carta[] CartasJogador { get; set; } 
        uint ValorPoteAteAgora { get; set; }
        IAcaoTomada AcaoTomada { get; set; }
    }
}