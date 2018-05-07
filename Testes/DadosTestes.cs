using DealerTH;
using Modelo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Testes
{
    public class DadosTeste
    {
        internal static MaoChecagem IniciaMaoChecagem(int id, IList<Carta> entrada)
        {
            MaoChecagem m = new MaoChecagem();
            m.iniciaArrays();

            switch (id)
            {
                case 1:
                    // Numeros
                    m.Numeros[0][0] = new Carta(1, Enuns.Naipe.Copas);
                    m.Numeros[0][1] = new Carta(1, Enuns.Naipe.Ouros);
                    m.Numeros[1][0] = new Carta(2, Enuns.Naipe.Copas);
                    m.Numeros[2][0] = new Carta(3, Enuns.Naipe.Copas);
                    m.Numeros[3][0] = new Carta(4, Enuns.Naipe.Copas);
                    m.Numeros[4][0] = new Carta(5, Enuns.Naipe.Copas);
                    m.Numeros[6][0] = new Carta(7, Enuns.Naipe.Paus);
                    m.Numeros[13][0] = new Carta(14, Enuns.Naipe.Ouros);
                    m.Numeros[13][1] = new Carta(14, Enuns.Naipe.Copas);

                    // Indice Numeros
                    m.indiceNumeros = new int[] { 2, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 2 };

                    // Naipes
                    m.Naipes[0][0] = m.Numeros[1][0];
                    m.Naipes[0][1] = m.Numeros[2][0];
                    m.Naipes[0][2] = m.Numeros[3][0];
                    m.Naipes[0][3] = m.Numeros[4][0];
                    m.Naipes[0][4] = m.Numeros[13][1];
                    m.Naipes[1][0] = m.Numeros[13][0];
                    m.Naipes[3][0] = m.Numeros[6][0];

                    // Indices Naipes
                    m.indiceNaipes = new int[] { 5, 1, 0, 1 };

                    return m;

                case 2:
                    // Numeros
                    m.Numeros[0][0] = new Carta(1, Enuns.Naipe.Copas);
                    m.Numeros[0][1] = new Carta(1, Enuns.Naipe.Ouros);
                    m.Numeros[1][0] = new Carta(2, Enuns.Naipe.Copas);
                    m.Numeros[2][0] = new Carta(3, Enuns.Naipe.Copas);
                    m.Numeros[3][0] = new Carta(4, Enuns.Naipe.Copas);
                    m.Numeros[5][0] = new Carta(6, Enuns.Naipe.Espadas);
                    m.Numeros[5][1] = new Carta(6, Enuns.Naipe.Paus);
                    m.Numeros[13][0] = new Carta(14, Enuns.Naipe.Ouros);
                    m.Numeros[13][1] = new Carta(14, Enuns.Naipe.Copas);

                    // Indice Numeros
                    m.indiceNumeros = new int[] { 2, 1, 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2 };

                    // Naipes
                    m.Naipes[0][0] = m.Numeros[1][0];
                    m.Naipes[0][1] = m.Numeros[2][0];
                    m.Naipes[0][2] = m.Numeros[3][0];
                    m.Naipes[0][4] = m.Numeros[13][1];
                    m.Naipes[1][0] = m.Numeros[13][0];
                    m.Naipes[2][0] = m.Numeros[5][0];
                    m.Naipes[3][0] = m.Numeros[5][1];

                    // Indices Naipes
                    m.indiceNaipes = new int[] { 4, 1, 1, 1 };

                    return m;

                case 3:
                    // Numeros
                    m.Numeros[2][0] = new Carta(3, Enuns.Naipe.Espadas);
                    m.Numeros[4][0] = new Carta(5, Enuns.Naipe.Copas);
                    m.Numeros[6][0] = new Carta(7, Enuns.Naipe.Copas);
                    m.Numeros[8][0] = new Carta(9, Enuns.Naipe.Paus);
                    m.Numeros[9][0] = new Carta(10, Enuns.Naipe.Paus);
                    m.Numeros[9][1] = new Carta(10, Enuns.Naipe.Espadas);
                    m.Numeros[9][2] = new Carta(10, Enuns.Naipe.Ouros);

                    // Indice Numeros
                    m.indiceNumeros = new int[] { 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 3, 0, 0, 0 };

                    // Naipes
                    m.Naipes[0][0] = m.Numeros[4][0];
                    m.Naipes[0][1] = m.Numeros[6][0];
                    m.Naipes[1][2] = m.Numeros[9][2];
                    m.Naipes[2][3] = m.Numeros[2][0];
                    m.Naipes[2][4] = m.Numeros[9][1];
                    m.Naipes[3][0] = m.Numeros[8][0];
                    m.Naipes[3][1] = m.Numeros[9][0];

                    // Indices Naipes
                    m.indiceNaipes = new int[] { 2, 1, 2, 2 };

                    return m;
                case 4:
                    // Numeros
                    m.Numeros[2][0] = new Carta(3, Enuns.Naipe.Espadas);
                    m.Numeros[4][0] = new Carta(5, Enuns.Naipe.Copas);
                    m.Numeros[6][0] = new Carta(7, Enuns.Naipe.Copas);
                    m.Numeros[7][0] = new Carta(8, Enuns.Naipe.Copas);
                    m.Numeros[8][0] = new Carta(9, Enuns.Naipe.Paus);
                    m.Numeros[9][0] = new Carta(10, Enuns.Naipe.Ouros);
                    m.Numeros[11][0] = new Carta(12, Enuns.Naipe.Paus);

                    // Indice Numeros
                    m.indiceNumeros = new int[] { 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0 };

                    // Naipes
                    m.Naipes[0][0] = m.Numeros[4][0];
                    m.Naipes[0][1] = m.Numeros[6][0];
                    m.Naipes[0][2] = m.Numeros[7][0];
                    m.Naipes[1][0] = m.Numeros[9][0];
                    m.Naipes[2][0] = m.Numeros[2][0];
                    m.Naipes[3][0] = m.Numeros[8][0];
                    m.Naipes[3][1] = m.Numeros[11][0];

                    // Indices Naipes
                    m.indiceNaipes = new int[] { 3, 1, 1, 2 };

                    return m;
                case 5:
                    // Numeros
                    m.Numeros[4][0] = new Carta(5, Enuns.Naipe.Copas);
                    m.Numeros[6][0] = new Carta(7, Enuns.Naipe.Copas);
                    m.Numeros[9][0] = new Carta(10, Enuns.Naipe.Copas);
                    m.Numeros[9][1] = new Carta(10, Enuns.Naipe.Paus);
                    m.Numeros[9][2] = new Carta(10, Enuns.Naipe.Espadas);
                    m.Numeros[10][0] = new Carta(11, Enuns.Naipe.Espadas);
                    m.Numeros[10][1] = new Carta(11, Enuns.Naipe.Paus);

                    // Indice Numeros
                    m.indiceNumeros = new int[] { 0, 0, 0, 0, 1, 0, 1, 0, 0, 3, 2, 0, 0, 0 };

                    // Naipes
                    m.Naipes[0][0] = m.Numeros[4][0];
                    m.Naipes[0][1] = m.Numeros[6][0];
                    m.Naipes[0][2] = m.Numeros[9][0];
                    m.Naipes[2][0] = m.Numeros[9][2];
                    m.Naipes[2][1] = m.Numeros[10][0];
                    m.Naipes[3][0] = m.Numeros[9][1];
                    m.Naipes[3][1] = m.Numeros[10][1];

                    // Indices Naipes
                    m.indiceNaipes = new int[] { 3, 0, 2, 2 };

                    return m;

                default: throw new NotImplementedException("Dados de teste não existem.");
            }
        }

        internal static void ImprimeArray<T>(T [][] Array1, T[][] Array2)
        {
            if (Array1.Length != Array2.Length)
                throw new Exception("Arrays com counts separados.");

            for (int i = 0; i < Array1.Length; i++)
                ImprimeArray<T>(Array1[i], Array2[i], i);
        }

        internal static void ImprimeArray<T>(T [] Array1, T [] Array2, int numLinha = -1)
        {
            if (Array1.Length != Array2.Length)
                throw new Exception("Arrays com counts separados.");

            for (int i = 0; i < Array1.Length; i++)
            {
                if (Array1[i] == null) return;
                Debug.WriteLine(Array1[i].ToString() + "\t" + Array2[i].ToString());
            }
        }
    }
}
