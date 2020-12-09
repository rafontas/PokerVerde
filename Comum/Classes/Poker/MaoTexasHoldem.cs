using Enuns;
using System;
using System.Collections.Generic;

namespace Modelo
{
    public class MaoTexasHoldem
    {
        public int IdMeuJogador { get; set; }

        public Jogo MeuJogo { get; set; } = Jogo.Nada;

        public IList<Carta> MelhorJogo { get; set; }

        public IList<Carta> Cartas { get; set; }

        public MaoTexasHoldem(Carta cartaUm, Carta cartaDois)
        {
            Cartas = new List<Carta>()
            {
                cartaUm,
                cartaDois
            };
        }

        public MaoTexasHoldem() { }

        /// <summary>
        /// Recebe as cartas da mão.
        /// </summary>
        /// <param name="cartaUm">Carta que receberá</param>
        /// <param name="cartaDois">Carta que receberá</param>
        public void RecebeCartas(Carta cartaUm, Carta cartaDois)
        {
            if (this.Cartas.Count == 2) throw new Exception("Essa mão já possui duas cartas.");

            Cartas = new List<Carta>()
            {
                cartaUm,
                cartaDois
            };
        }

        public override bool Equals(object other)
        {
            var OutraMao = other as MaoTexasHoldem;
            if (MeuJogo != OutraMao.MeuJogo) return false;

            foreach (Carta c in OutraMao.MelhorJogo)
                if (!MelhorJogo.Contains(c)) return false;

            return true;
        }

        public int Compara(MaoTexasHoldem outra)
        {
            if (MeuJogo > outra.MeuJogo) return 1;
            else if (MeuJogo < outra.MeuJogo) return -1;
            else
            {
                if (MeuJogo == Jogo.Dupla)
                {
                    // Avalia a dupla
                    if (MelhorJogo[1].Numero > outra.MelhorJogo[1].Numero) return 1;
                    else if (MelhorJogo[1].Numero < outra.MelhorJogo[1].Numero) return -1;
                    // Avalia os kickers
                    else if (MelhorJogo[2].Numero > outra.MelhorJogo[2].Numero) return 1;
                    else if (MelhorJogo[2].Numero < outra.MelhorJogo[2].Numero) return -1;
                    else if (MelhorJogo[3].Numero > outra.MelhorJogo[3].Numero) return 1;
                    else if (MelhorJogo[3].Numero < outra.MelhorJogo[3].Numero) return -1;
                    else if (MelhorJogo[4].Numero > outra.MelhorJogo[4].Numero) return 1;
                    else if (MelhorJogo[4].Numero < outra.MelhorJogo[4].Numero) return -1;
                    else return 0;
                }
                else if (MeuJogo == Jogo.CartaAlta)
                {
                    // Avalia os kickers
                    if (MelhorJogo[0].Numero > outra.MelhorJogo[0].Numero) return 1;
                    else if (MelhorJogo[0].Numero < outra.MelhorJogo[0].Numero) return -1;
                    else if (MelhorJogo[1].Numero > outra.MelhorJogo[1].Numero) return 1;
                    else if (MelhorJogo[1].Numero < outra.MelhorJogo[1].Numero) return -1;
                    else if (MelhorJogo[2].Numero > outra.MelhorJogo[2].Numero) return 1;
                    else if (MelhorJogo[2].Numero < outra.MelhorJogo[2].Numero) return -1;
                    else if (MelhorJogo[3].Numero > outra.MelhorJogo[3].Numero) return 1;
                    else if (MelhorJogo[3].Numero < outra.MelhorJogo[3].Numero) return -1;
                    else if (MelhorJogo[4].Numero > outra.MelhorJogo[4].Numero) return 1;
                    else if (MelhorJogo[4].Numero < outra.MelhorJogo[4].Numero) return -1;
                    else return 0;
                }
                else if (MeuJogo == Jogo.DuasDuplas)
                {
                    // Avalia dupla maior
                    if (MelhorJogo[0].Numero > outra.MelhorJogo[0].Numero) return 1;
                    else if (MelhorJogo[0].Numero < outra.MelhorJogo[0].Numero) return -1;
                    // Avalia dupla menor
                    else if (MelhorJogo[2].Numero > outra.MelhorJogo[2].Numero) return 1;
                    else if (MelhorJogo[2].Numero < outra.MelhorJogo[2].Numero) return -1;
                    // Avalia os kickers
                    else if (MelhorJogo[4].Numero > outra.MelhorJogo[4].Numero) return 1;
                    else if (MelhorJogo[4].Numero < outra.MelhorJogo[4].Numero) return -1;
                    else return 0;
                }
                else if (MeuJogo == Jogo.Trinca)
                {
                    // Avalia a trinca
                    if (MelhorJogo[0].Numero > outra.MelhorJogo[0].Numero) return 1;
                    else if (MelhorJogo[0].Numero < outra.MelhorJogo[0].Numero) return -1;
                    // Avalia os kickers
                    else if (MelhorJogo[3].Numero > outra.MelhorJogo[3].Numero) return 1;
                    else if (MelhorJogo[3].Numero < outra.MelhorJogo[3].Numero) return -1;
                    else if (MelhorJogo[4].Numero > outra.MelhorJogo[4].Numero) return 1;
                    else if (MelhorJogo[4].Numero < outra.MelhorJogo[4].Numero) return -1;
                    else return 0;
                }
                else if (MeuJogo == Jogo.Straight)
                {
                    // Avalia o maior numero
                    if (MelhorJogo[0].Numero > outra.MelhorJogo[0].Numero) return 1;
                    else if (MelhorJogo[0].Numero < outra.MelhorJogo[0].Numero) return -1;
                    else return 0;
                }
                else if (MeuJogo == Jogo.Flush)
                {
                    // Avalia o kicker
                    if (MelhorJogo[0].Numero > outra.MelhorJogo[0].Numero) return 1;
                    else return -1;
                }
                else if (MeuJogo == Jogo.FullHand)
                {
                    // Avalia os kickers
                    if (MelhorJogo[0].Numero > outra.MelhorJogo[0].Numero) return 1;
                    else if (MelhorJogo[0].Numero < outra.MelhorJogo[0].Numero) return -1;
                    else if (MelhorJogo[3].Numero > outra.MelhorJogo[3].Numero) return 1;
                    else if (MelhorJogo[3].Numero < outra.MelhorJogo[3].Numero) return -1;
                    else return 0;
                }
                else
                {
                    // Avalia os kickers do straight flush
                    if (MelhorJogo[0].Numero > outra.MelhorJogo[0].Numero) return 1;
                    else if (MelhorJogo[0].Numero < outra.MelhorJogo[0].Numero) return -1;
                    else return 0;
                }
            }
        }

        public override int GetHashCode()
        {
            int hash = MelhorJogo[0].GetHashCode();
            for (int i = 1; i < MelhorJogo.Count; i++)
                hash ^= MelhorJogo[i].GetHashCode();
            return hash;
        }
    
    }
}
