using Enuns;
using Modelo;
using System.Linq;
using System.Collections.Generic;

namespace DealerTH
{
    internal class Dupla : IJogo
    {
        IList<Carta> _Dupla { get; set; }
        IList<Carta> RestoMao { get; set; }

        public Jogo Identifique() => Jogo.Dupla;

        public IList<Carta> Kickers()
        {
            RestoMao = RestoMao.OrderByDescending(c => c.Numero).ToList();
            return new List<Carta>() {
                _Dupla[0],
                RestoMao[0],
                RestoMao[1],
                RestoMao[2],
            };
        }

        public uint Valor() => _Dupla.First().Numero;
    }
}
