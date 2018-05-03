using Enuns;
using Modelo;
using System.Linq;
using System.Collections.Generic;

namespace DealerTH
{
    internal class StraigthFlush : IJogo
    {
        IList<Carta> _Straight { get; set; }

        public Jogo Identifique() => Jogo.StraightFlush;

        public IList<Carta> Kickers() => _Straight.OrderByDescending(c => c.Numero).ToList();

        public uint Valor() => (uint) Jogo.StraightFlush;
    }
}
