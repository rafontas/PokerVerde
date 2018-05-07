using Modelo;
using System;
using System.Linq;
using System.Collections.Generic;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Testes")]
namespace DealerTH
{
    internal class MaoChecagem
    {
        // Usados para identificar straigth flush
        private int CountCartas = 7;
        internal int[] sequenciaNaipe;
        internal Carta[] cartas;
        internal Carta[] kickers = new Carta[7];
        internal int indiceKicker = 0;

        internal IList<Carta> _Straight = new List<Carta>();
        internal IList<Carta> [] _StraightFlush = new IList<Carta>[4] {
            new List<Carta>(),
            new List<Carta>(),
            new List<Carta>(),
            new List<Carta>(),
         };

        internal Carta[][] Numeros = new Carta[14][];
        internal Carta[][] Naipes = new Carta[4][];
        internal int[] indiceNumeros = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        internal int[] indiceNaipes = new int[] { 0, 0, 0, 0 };

        internal bool Dupla { get { return indiceDuplaUm > -1; } }
        internal bool DuasDuplas { get { return indiceDuplaDois > -1; } }
        internal bool Trinca { get { return indiceTrinca > -1; } }
        internal bool Straight { get { return indiceStraigth > -1; } }
        internal bool Flush { get { return indiceFlush > -1; } }
        internal bool FullHand { get { return indiceFullHand > -1; } }
        internal bool Four { get { return indiceFour > -1; } }
        internal bool StraightFlush { get { return indiceStraightFlush > -1; } }

        int indiceDuplaUm = -1;
        int indiceDuplaDois = -1;
        int indiceTrinca = -1;
        int indiceStraigth = -1;
        int indiceFlush = -1;
        int indiceFullHand = -1;
        int indiceFour = -1;
        int indiceStraightFlush = -1;
        int indiceCartaMaisAlta = 0;

        internal void iniciaArrays()
        {
            sequenciaNaipe = new int[4];

            for (int i = 0; i < 14; i++)
            {
                if (i < 4)
                {
                    Naipes[i] = new Carta[7];
                    sequenciaNaipe[i] = 0;
                }
                Numeros[i] = new Carta[7];
            }
        }

        internal MaoChecagem() { }

        internal MaoChecagem(IList<Carta> cartas)
        {
            int sequencia = 1;
            int sequenciaFlush = 0;
            int naipeMomento = 0;
            iniciaArrays();
            IList<Carta> cartasAtualizadas = new List<Carta>();

            cartas = cartas.OrderBy(c => c.Numero).ToArray();
            int inicio = 0;

            if (cartas[6].Numero == 14)
            {
                Carta novoAs = new Carta(1, cartas[6].Naipe);
                cartasAtualizadas.Add(novoAs);
                naipeMomento = (int)cartas[6].Naipe - 1;
                Numeros[0][indiceNumeros[0]++] = novoAs;
                sequenciaNaipe[naipeMomento] = 1;
                inicio++;

                _StraightFlush[naipeMomento].Add(novoAs);
                _Straight.Add(novoAs);

                for (int i = 5; i > -1; i--)
                {
                    if (cartas[i].Numero == 14)
                    {
                        naipeMomento = (int)cartas[i].Naipe - 1;
                        novoAs = new Carta(1, cartas[i].Naipe);
                        cartasAtualizadas.Add(novoAs);
                        sequenciaNaipe[naipeMomento] = 1;

                        _StraightFlush[naipeMomento].Add(novoAs);

                        Numeros[0][indiceNumeros[0]++] = novoAs;
                        inicio++;
                    }
                    else
                        break;
                }
            }

            int numCartas = cartas.Count;

            for (int i = 0; i < cartas.Count; i++)
            {
                if (numCartas == 0) return;

                naipeMomento = (int)cartas[i].Naipe - 1;
                uint indice = cartas[i].Numero - 1;
                Numeros[indice][indiceNumeros[indice]++] = cartas[i];
                Naipes[naipeMomento][indiceNaipes[naipeMomento]++] = cartas[i];
                numCartas--;
                indiceCartaMaisAlta = i;

                // Identifica Flush
                if (indiceNaipes[naipeMomento] >= 5) indiceFlush = i;

                // Verifica se tem AS.
                if (cartas[i].Numero == 2
                    && (sequenciaNaipe[0] + sequenciaNaipe[1] + sequenciaNaipe[2] + sequenciaNaipe[3]) > 1)
                {
                    sequencia++;

                    // Se tiver As do mesmo naipe
                    if (Naipes[naipeMomento][0] != null) sequenciaNaipe[naipeMomento]++;
                }
                // Isso é pra avaliar straigth flush e straights
                else if (i == 0) {
                    sequencia = 1;
                    sequenciaNaipe[naipeMomento] = 1;
                }
                else if (cartas[i - 1].Numero == (cartas[i].Numero - 1))
                {
                    sequencia++;
                    if (sequencia >= 5) indiceStraigth = i;

                    _Straight.Add(cartas[i]);

                    // Verifica a sequencia dos naipes
                    if (indiceNaipes[naipeMomento] > 1)
                    {
                        if (cartas[i].Naipe == Naipes[naipeMomento][indiceNaipes[naipeMomento] - 1].Naipe)
                        {
                            _StraightFlush[naipeMomento].Add(cartas[i]);
                            sequenciaNaipe[naipeMomento]++;

                            if (sequenciaNaipe[naipeMomento] >= 5)
                            {
                                sequenciaFlush = i;
                                indiceStraightFlush = naipeMomento;
                            }
                        }
                    }
                    else
                    {
                        sequenciaNaipe[naipeMomento] = 1;
                        _StraightFlush[naipeMomento] = new List<Carta>();
                    }

                }
                else
                {
                    sequencia = 1;
                    sequenciaNaipe[naipeMomento] = 0;

                    _StraightFlush[naipeMomento] = new List<Carta>();
                    _Straight = new List<Carta>();
                }
            }

            IdentificaDuplasTrincasFour();
        }

        internal void IdentificaDuplasTrincasFour()
        {
            indiceCartaMaisAlta = CountCartas;
            int NumeroCarta = 1;

            for (int i = 1; i < 14; i++)
            {
                switch (indiceNumeros[i])
                {
                    case 0: continue;
                    case 1:
                        NumeroCarta++;
                        kickers[indiceKicker++] = Numeros[i][0];
                        break;
                    case 2:
                        NumeroCarta += 2;
                        if (indiceDuplaUm > 0 && i != 0)
                        {
                            indiceDuplaDois = indiceDuplaUm;
                            indiceDuplaUm = i;
                        }
                        else { indiceDuplaUm = i; }
                        break;
                    case 3:
                        NumeroCarta += 3; indiceTrinca = i;
                        if (indiceDuplaUm > 0 && i != 0)
                        {
                            indiceDuplaDois = indiceDuplaUm;
                            indiceDuplaUm = i;
                        }
                        else { indiceDuplaUm = i; }
                        break;
                    case 4:
                        NumeroCarta += 4; indiceFour = i; break;
                    default:
                        throw new Exception("Identificado mais que 4 cartas de um mesmo número.");
                }

                if (NumeroCarta == CountCartas) break;
            }
        }

        private MaoTexasHoldem SelecionaMelhorMao()
        {
            if (indiceStraightFlush != -1)
            {
                return new MaoTexasHoldem()
                {
                    
                };    
            }
            else if (indiceFour != -1)
            {

            }
            else if (indiceTrinca != -1 && (indiceDuplaDois != -1 || indiceDuplaUm != -1))
            {

            }
            else if (indiceStraightFlush != -1)
            {

            }
            else if (indiceStraightFlush != -1)
            {

            }
            else if (indiceStraightFlush != -1)
            {

            }
            else if (indiceStraightFlush != -1)
            {

            }
            else if (indiceStraightFlush != -1)
            {

            }
        }
    }
}
