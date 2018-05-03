using Enuns;
using Modelo;
using System.Linq;
using System.Collections.Generic;

namespace DealerTH
{
    internal class Trinca : IJogo
    {
        IList<Carta> _Trinca { get; set; }
        IList<Carta> _Kickers { get; set; }

        public Jogo Identifique() => Jogo.Trinca;
        
        public IList<Carta> Kickers()
        {
            _Kickers = _Kickers.OrderByDescending(c => c.Numero).ToList();
            IList<Carta> k = new List<Carta>()
            {
                _Trinca[0],
                _Kickers[0],
                _Kickers[1],
            };

            return k;
        }

        public uint Valor() => (uint) Jogo.Trinca;
    }
}
