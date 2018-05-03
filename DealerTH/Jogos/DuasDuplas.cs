using Enuns;
using Modelo;
using System.Collections.Generic;
using System.Linq;

namespace DealerTH
{
    internal class DuasDuplas : IJogo
    {
        IList<Carta> DuplaUm { get; set; }
        IList<Carta> DuplaDois { get; set; }
        Carta Kicker { get; set; }

        public Carta CartaAlta() => Kicker;

        public Jogo Identifique() => Jogo.DuasDuplas;

        public IList<Carta> Kickers() {
            IList<Carta> k = new List<Carta>()
            {
                DuplaUm[0],
                DuplaDois[0],
            };

            k = k.OrderByDescending(c => c.Numero).ToList();

            k.Add(Kicker);

            return k;
        }

        public uint Valor() => (int) Jogo.DuasDuplas;
    }
}
