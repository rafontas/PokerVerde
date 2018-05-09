using Modelo;
using System;
using System.Linq;
using System.Collections.Generic;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Testes")]
namespace DealerTH
{
    internal class MelhorMao
    {
        IList<Carta> Cartas;
        IList<IList<Carta>> Naipes = new List<IList<Carta>>()
        {
            new List<Carta>(),
            new List<Carta>(),
            new List<Carta>(),
            new List<Carta>(),
        };

        IList<Carta[]> Duplas = new List<Carta[]>();
        IList<Carta[]> Trincas = new List<Carta[]>();
        IList<Carta> Four = new List<Carta>();

        internal bool TemDupla { get { return Duplas.Count >= 1; } }
        internal bool TemDuasDuplas { get { return Duplas.Count >= 2; } }
        internal bool TemTrinca { get { return Trincas.Count > 0; } }
        internal bool TemDuasTrincas { get { return Trincas.Count > 1; } }
        internal bool TemFlush { get; set; }
        internal bool TemStraight { get; set; }
        internal bool TemFour { get { return Four.Count == 4; } }
        internal bool TemStraightFlush { get; set; }

        internal MaoTexasHoldem AvaliaMao(IList<Carta> CartasAvaliar)
        {
            Cartas = CartasAvaliar.OrderBy(c => c.Numero).ToList();

            // Monta a estrutura de Naipe
            MontaNaipes(Cartas);
            
            if(GetStraigthFlush(Cartas, out IList<Carta> Mao))
            {
                return new MaoTexasHoldem()
                {
                    MeuJogo = Enuns.Jogo.StraightFlush,
                    MelhorJogo = Mao
                };
            }
            else if (TemFour)
            {
                Carta Kicker = GetListaKicker(Cartas, Four, 1).First();
                Four.Add(Kicker);

                return new MaoTexasHoldem()
                {
                    MeuJogo = Enuns.Jogo.Four,
                    MelhorJogo = Four
                };
            }
            else if ((TemTrinca && TemDupla) || TemDuasTrincas)
            {
                if (TemTrinca && TemDupla)
                {
                    return new MaoTexasHoldem()
                    {
                        MeuJogo = Enuns.Jogo.FullHand,
                        MelhorJogo = new List<Carta>()
                        {
                            Trincas[0][0],
                            Trincas[0][1],
                            Trincas[0][2],
                            Duplas[0][0],
                            Duplas[0][1],
                        }
                    };
                }
                else
                {
                    return new MaoTexasHoldem()
                    {
                        MeuJogo = Enuns.Jogo.FullHand,
                        MelhorJogo = new List<Carta>()
                        {
                            Trincas[0][0],
                            Trincas[0][1],
                            Trincas[0][2],
                            Trincas[1][0],
                            Trincas[1][1],
                        }
                    };
                }

            }
            else if (GetFlush(Cartas, out Mao))
            {
                return new MaoTexasHoldem()
                {
                    MeuJogo = Enuns.Jogo.Flush,
                    MelhorJogo = Mao
                };
            }
            else if (GetStraigth(Cartas, out Mao))
            {
                return new MaoTexasHoldem()
                {
                    MeuJogo = Enuns.Jogo.Straight,
                    MelhorJogo = Mao
                };

            }
            else if (TemTrinca)
            {
                IList<Carta> Kickers = GetListaKicker(Cartas, Four, 2);

                return new MaoTexasHoldem()
                {
                    MeuJogo = Enuns.Jogo.Trinca,
                    MelhorJogo = new List<Carta>()
                    {
                        Trincas[0][0],
                        Trincas[0][1],
                        Trincas[0][2],
                        Kickers[0],
                        Kickers[1],
                    }
                };
            }
            else if (TemDuasDuplas)
            {
                IList<Carta> Kickers = GetListaKicker(Cartas, Four, 1);

                return new MaoTexasHoldem()
                {
                    MeuJogo = Enuns.Jogo.DuasDuplas,
                    MelhorJogo = new List<Carta>()
                    {
                        Duplas[0][0],
                        Duplas[0][1],
                        Duplas[1][0],
                        Duplas[1][1],
                        Kickers[0],
                    }
                };
            }
            else if (TemDupla)
            {
                IList<Carta> Kickers = GetListaKicker(Cartas, Four, 3);

                return new MaoTexasHoldem()
                {
                    MeuJogo = Enuns.Jogo.Dupla,
                    MelhorJogo = new List<Carta>()
                    {
                        Duplas[0][0],
                        Duplas[0][1],
                        Kickers[0],
                        Kickers[1],
                        Kickers[2],
                    }
                };
            }
            else
            {
                IList<Carta> Kickers = GetListaKicker(Cartas, Four, 5);

                return new MaoTexasHoldem()
                {
                    MeuJogo = Enuns.Jogo.CartaAlta,
                    MelhorJogo = new List<Carta>()
                    {
                        Kickers[0],
                        Kickers[1],
                        Kickers[2],
                        Kickers[3],
                        Kickers[4],
                    }
                };
            }
        }

        internal bool GetStraigthFlush(IList<Carta> CartasAvaliar, out IList<Carta> StraightFlush)
        {
            if(Naipes[0].Count >= 5)
            {
                if (GetStraigthInverso(Naipes[0], out StraightFlush)) return true;
            }
            else if (Naipes[1].Count >= 5)
            {
                if (GetStraigthInverso(Naipes[1], out StraightFlush)) return true;
            }
            else if (Naipes[2].Count >= 5)
            {
                if (GetStraigthInverso(Naipes[2], out StraightFlush)) return true;
            }
            else if (Naipes[3].Count >= 5)
            {
                if (GetStraigthInverso(Naipes[3], out StraightFlush)) return true;
            }

            StraightFlush = null;
            return false;
        }

        private void MontaNaipes(IList<Carta> CartasAvaliarOrdenada)
        {
            int DuplaControle = 0;

            for (int i = CartasAvaliarOrdenada.Count-1; i > -1; i--)
            {
                // Avalia Four
                if ((i - 3) >= 0)
                {
                    // Verifica Four
                    if (CartasAvaliarOrdenada[i].Numero == CartasAvaliarOrdenada[i - 1].Numero 
                        && CartasAvaliarOrdenada[i - 1].Numero == CartasAvaliarOrdenada[i - 2].Numero
                        && CartasAvaliarOrdenada[i - 2].Numero == CartasAvaliarOrdenada[i - 3].Numero)
                    {
                        Four.Add(CartasAvaliarOrdenada[i]);
                        Four.Add(CartasAvaliarOrdenada[i-1]);
                        Four.Add(CartasAvaliarOrdenada[i-2]);
                        Four.Add(CartasAvaliarOrdenada[i-3]);
                        return;
                    }
                }

                // Avalia Trinca
                if ((i-2) >= 0)
                {
                    if(CartasAvaliarOrdenada[i].Numero == CartasAvaliarOrdenada[i-1].Numero &&
                       CartasAvaliarOrdenada[i-1].Numero == CartasAvaliarOrdenada[i - 2].Numero)
                    {
                        Trincas.Add(new Carta[] {
                            CartasAvaliarOrdenada[i],
                            CartasAvaliarOrdenada[i-1],
                            CartasAvaliarOrdenada[i-2],
                        });
                        DuplaControle = 2;
                    }
                }

                // Avalia Dupla
                if ((i - 1) >= 0 
                    && DuplaControle <= 0 
                    && CartasAvaliarOrdenada[i].Numero == CartasAvaliarOrdenada[i - 1].Numero)
                {
                    Duplas.Add(new Carta[] {
                            CartasAvaliarOrdenada[i],
                            CartasAvaliarOrdenada[i-1],
                    });
                }
                else
                    DuplaControle--;

                int NaipeMomento = (int) CartasAvaliarOrdenada[i].Naipe-1;
                Naipes[NaipeMomento].Add(CartasAvaliarOrdenada[i]);
            }
        }

        internal bool GetStraigth(IList<Carta> CartasAvaliar, out IList<Carta> Straight)
        {
            Straight  = new List<Carta>();
            int NumAs = CartasAvaliar.Count-1;

            // Corrige o Problema do As
            IList<Carta> CartasAvaliarAuxiliar = new List<Carta>();
            while(CartasAvaliar[NumAs].Numero == 14)
            {
                CartasAvaliarAuxiliar.Add(
                    new Carta(1, CartasAvaliar[NumAs].Naipe)
                );
                NumAs--;
            }
            if(NumAs != CartasAvaliar.Count - 1)
            {
                CartasAvaliarAuxiliar = CartasAvaliarAuxiliar.Concat(CartasAvaliar).ToList();
            }
            else
            {
                CartasAvaliarAuxiliar = CartasAvaliar;
            }

            Straight.Add(CartasAvaliarAuxiliar[CartasAvaliarAuxiliar.Count - 1]);

            for (int i = CartasAvaliarAuxiliar.Count -1; i > -1; i--)
            {
                if (i == 0)
                {
                    Straight = new List<Carta>();
                    return false;
                }

                // Ignora cartas iguais
                if (CartasAvaliarAuxiliar[i].Numero == CartasAvaliarAuxiliar[i-1].Numero)
                    continue;

                if (CartasAvaliarAuxiliar[i].Numero == CartasAvaliarAuxiliar[i-1].Numero+1)
                {
                    Straight.Add(CartasAvaliarAuxiliar[i-1]);
                    if (Straight.Count == 5) return true;
                }
                else
                {
                    if (i < 4) return false;
                    Straight = new List<Carta>() { CartasAvaliarAuxiliar[i-1] };
                }
            }

            return false;
        }

        internal bool GetStraigthInverso(IList<Carta> CartasAvaliar, out IList<Carta> Straight)
        {
            Straight = new List<Carta>();
            int NumAs = 0;

            // Corrige o Problema do As
            IList<Carta> CartasAvaliarAuxiliar = new List<Carta>();
            while (CartasAvaliar[NumAs].Numero == 14)
            {
                CartasAvaliarAuxiliar.Add(
                    new Carta(1, CartasAvaliar[NumAs].Naipe)
                );
                NumAs++;
            }
            if (NumAs != CartasAvaliar.Count - 1)
                CartasAvaliarAuxiliar = CartasAvaliarAuxiliar.Concat(CartasAvaliar).ToList();

            CartasAvaliarAuxiliar = CartasAvaliarAuxiliar.OrderBy(c => c.Numero).ToList();
            Straight.Add(CartasAvaliarAuxiliar[CartasAvaliarAuxiliar.Count - 1]);

            for (int i = CartasAvaliarAuxiliar.Count - 1; i > -1; i--)
            {
                if(i == 0)
                {
                    CartasAvaliarAuxiliar = new List<Carta>();
                    return false;
                }

                // Ignora cartas iguais
                if (CartasAvaliarAuxiliar[i].Numero == CartasAvaliarAuxiliar[i - 1].Numero) continue;

                if (CartasAvaliarAuxiliar[i].Numero == CartasAvaliarAuxiliar[i - 1].Numero + 1)
                {
                    Straight.Add(CartasAvaliarAuxiliar[i-1]);
                    if (Straight.Count == 5) return true;
                }
                else
                {
                    if (i < 4) return false;
                    Straight = new List<Carta>() { CartasAvaliarAuxiliar[i-1] };
                }
            }

            return false;
        }

        internal bool GetFlush(IList<Carta> CartasAvaliar, out IList<Carta> Flush)
        {
            int naipeFlush = -1;

            if (Naipes[0].Count >= 5) { naipeFlush = 0; }
            else if (Naipes[1].Count >= 5) { naipeFlush = 1; }
            else if (Naipes[2].Count >= 5) { naipeFlush = 2; }
            else if (Naipes[3].Count >= 5) { naipeFlush = 3; }

            if (naipeFlush != -1)
            {
                 Flush = new List<Carta>() {
                        Naipes[naipeFlush][0],
                        Naipes[naipeFlush][1],
                        Naipes[naipeFlush][2],
                        Naipes[naipeFlush][3],
                        Naipes[naipeFlush][4],
                };

                return true;
            }
            else
            {
                Flush = null;
                return false;
            }
        }

        private IList<Carta> GetListaKicker(IList<Carta> CartasOrdenadasCrescente, IList<Carta> Excecoes = null, int quantidade = -1)
        {
            IList<Carta> Kickers = new List<Carta>();
            for (int i = CartasOrdenadasCrescente.Count-1; i > -1; i--)
            {
                if (Excecoes.Contains(CartasOrdenadasCrescente[i])) continue;
                Kickers.Add(CartasOrdenadasCrescente[i]);

                if (quantidade != -1 && Kickers.Count == quantidade) return Kickers;
            }

            return Kickers;
        }
    }
}
