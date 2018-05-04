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

        public static IList<Carta> Encontra(Carta[] Cartas)
        {
            Carta[][] flush = new Carta[][] {
                new Carta[] { },
                new Carta[] { },
                new Carta[] { },
                new Carta[] { },
            };
            int[] counts = new int[] { 0, 0, 0, 0 };

            int i = 0;
            for (i = 0; i < 7; i++)
            {
                switch (Cartas[i].Naipe)
                {
                    case Naipe.Copas:
                        flush[0][counts[0]] = Cartas[i];
                        counts[0]++; break;

                    case Naipe.Espadas:
                        flush[1][counts[1]] = Cartas[i];
                        counts[1]++; break;

                    case Naipe.Ouros:
                        flush[2][counts[2]] = Cartas[i];
                        counts[2]++; break;

                    case Naipe.Paus:
                        flush[3][counts[3]] = Cartas[i];
                        counts[3]++; break;
                    default: throw new Exception("Naipe Inválido!");
                }
            }

            if(counts[0] >= 5)
            {
                counts[0]--;
                return new List<Carta>()
                {
                    flush[0][counts[0]--],
                    flush[0][counts[0]--],
                    flush[0][counts[0]--],
                    flush[0][counts[0]--],
                    flush[0][counts[0]--],
                };
            }
            else if (counts[1] >= 5)
            {
                counts[1]--;
                return new List<Carta>()
                {
                    flush[0][counts[1]--],
                    flush[0][counts[1]--],
                    flush[0][counts[1]--],
                    flush[0][counts[1]--],
                    flush[0][counts[1]--],
                };
            }
            else if (counts[2] >= 5) {
                counts[2]--;
                return new List<Carta>()
                {
                    flush[0][counts[2]--],
                    flush[0][counts[2]--],
                    flush[0][counts[2]--],
                    flush[0][counts[2]--],
                    flush[0][counts[2]--],
                };

            }
            else if (counts[3] >= 5) {
                counts[3]--;
                return new List<Carta>()
                {
                    flush[0][counts[3]--],
                    flush[0][counts[3]--],
                    flush[0][counts[3]--],
                    flush[0][counts[3]--],
                    flush[0][counts[3]--],
                };

            }

            return null;
        }

    }
}
