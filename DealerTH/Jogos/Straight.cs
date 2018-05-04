using Enuns;
using Modelo;
using System.Linq;
using System.Collections.Generic;

namespace DealerTH
{
    internal class Straight : IJogo
    {
        IList<Carta> _Straight { get; set; }

        public Jogo Identifique() => Jogo.Straight;

        public IList<Carta> Kickers() => _Straight.OrderByDescending(c => c.Numero).ToList();

        public uint Valor() => (uint) Jogo.Straight;

        public static IList<Carta> Encontra(Carta [] Cartas)
        {
            bool sequencia = false;
            uint numSeguidos = 1;
            uint limite = 7;
            int kickerIndice = 0;

            Carta[] mao = Cartas;

            if (Cartas[7].Numero == 14)
            {
                mao = new Carta[] 
                {
                    new Carta(1, Cartas[7].Naipe),
                    Cartas[0],
                    Cartas[1],
                    Cartas[2],
                    Cartas[3],
                    Cartas[4],
                    Cartas[5],
                    Cartas[6],
                };
                limite = 8;
            }
            int i = 1;

            for (i = 1; i < limite; i++)
            {
                if ((mao[i - 1].Numero + 1) == mao[i].Numero)
                {
                    if (++numSeguidos >= 5)
                    {
                        kickerIndice = i;
                        sequencia = true;
                    }
                }
                else if (mao[i-1].Numero != mao[i].Numero)
                    numSeguidos = 1;
            }

            if (sequencia) {
                List<Carta> _sequencia = new List<Carta>() {
                    mao[kickerIndice]
                };
                Carta ultimaCarta = mao[kickerIndice];
                    
                for (int j = kickerIndice-1; j > -1; j--)
                {
                    if (ultimaCarta.Numero != mao[j].Numero)
                        _sequencia.Add(mao[j]);

                    ultimaCarta = mao[j];
                }

                return _sequencia;
            }

            return null;
        }
    }
}
