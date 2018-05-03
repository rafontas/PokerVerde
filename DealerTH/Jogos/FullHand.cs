using Enuns;
using Modelo;
using System.Collections.Generic;

namespace DealerTH
{
    internal class FullHand : IJogo
    {
        IList<Carta> _Trinca { get; set; }
        IList<Carta> _Dupla { get; set; }

        public Jogo Identifique() => Jogo.Trinca;

        public IList<Carta> Kickers() => new List<Carta>() { _Trinca[0], _Dupla[0] };

        public uint Valor() => (uint) Jogo.Trinca;
    }
}
