using Modelo;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DealerTH
{
    internal class MaoChecagem
    {
        internal Carta[][] Numeros = new Carta[14][];
        internal Carta[][] Naipes = new Carta[4][];
        internal int[] indiceNumeros = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal int[] indiceNaipes = new int[] { 0, 0, 0, 0 };

        private void iniciaArrays()
        {
            for (int i = 0; i < 14; i++)
            {
                if(i < 4) Naipes[i] = new Carta[7];
                Numeros[i] = new Carta[7];
            }
        }

        internal MaoChecagem(IList<Carta> cartas)
        {
            iniciaArrays();

            Carta[] cs = cartas.OrderBy(c => c.Numero).ToArray();
            int inicio = cs[6].Numero == 14 ? 0 : 1;
            int numCartas = 0;

            if(cs[6].Numero == 14)
            {
                Numeros[0][indiceNumeros[0]++] = new Carta(1, cs[6].Naipe);
                numCartas++;

                for (int i = 5; i > -1; i--)
                {
                    if (cs[i].Numero == 14)
                    {
                        Numeros[0][indiceNumeros[0]++] = new Carta(1, cs[6].Naipe);
                        numCartas++;
                    }
                    else
                        break;
                }
            }

            for (int i = inicio; i < 7; i++)
            {
                if (numCartas == 7) return;

                uint indice = cs[i].Numero - 1;
                Numeros[indice][indiceNumeros[indice]++] = cs[i];
                Naipes[(uint)cs[i].Naipe - 1][indiceNaipes[(uint)cs[i].Naipe - 1]] = cs[i];
                numCartas++;
            }
        }

        internal IJogo MaoMaisForte()
        {
            for (int i = 0; i < 14; i++)
            {

            }
        }
    }
}
