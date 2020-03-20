using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.AbstractClasses
{
    public abstract class RodadaBase : IRodada
    {
        
        public Carta[] CartasMesa { get; set; }
        public Carta[] CartasJogador { get; set; }
        public uint ValorPoteAteAgora { get; set; }
        public IAcaoTomada AcaoTomada { get; set; }
        public TipoRodada TipoRodada { get; protected set; }

        public RodadaBase(Carta [] cartaMesa, uint valorPote, TipoRodada tipo)
        {
            this.TipoRodada = tipo;
            this.CartasMesa = cartaMesa;
            this.ValorPoteAteAgora = valorPote;
        }
    }
}
