using Comum.AbstractClasses;
using Enuns;
using Modelo;

namespace Comum.Classes
{
    public class RodadaTHB : RodadaBase
    {
        public uint ValorApostadoRodadaJogador { get; set; }
        public uint ValorApostadoRodadaBanca { get; set; }

        public RodadaTHB(TipoRodada tp, uint valorPotePreRodada, Carta[] cartaMesa) : base(cartaMesa, valorPotePreRodada, tp) 
        {
            
        }
    }
}
