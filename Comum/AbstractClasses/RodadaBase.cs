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
        public uint ValorPotePreRodada { get; set; }
        public TipoRodada TipoRodada { get; protected set; }

        public RodadaBase(Carta [] cartaMesa, uint valorPotePreRodada, TipoRodada tipo)
        {
            this.TipoRodada = tipo;
            this.CartasMesa = cartaMesa;
            this.ValorPotePreRodada = valorPotePreRodada;
        }
    }
}
