using Enuns;
using Modelo;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DealerTH
{
    internal class Flush : IJogo
    {
        IList<Carta> _Flush { get; set; }

        public Jogo Identifique() => Jogo.Flush;

        public IList<Carta> Kickers() => _Flush.OrderByDescending(c => c.Numero).ToList();

        public uint Valor() => (uint) Jogo.Flush;
    }
}
