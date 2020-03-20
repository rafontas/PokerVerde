using Comum.AbstractClasses;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes
{
    public class RodadaTHB : RodadaBase
    {
        public AcoesDecisaoJogador action { get; set; }
        public uint valor { get; set; }
        public Carta[] cartaPlayer;
        public Carta[] cartaMesa;
        public RodadaTHB(TipoRodada tp, uint valorPote, Carta[] cartaMesa) : base(cartaMesa, valorPote, tp) { }
    }
}
