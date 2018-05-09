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

        public MaoTexasHoldem() {}

        /// <summary>
        /// Recebe as cartas da mão.
        /// </summary>
        /// <param name="cartaUm">Carta que receberá</param>
        /// <param name="cartaDois">Carta que receberá</param>
        public void RecebeCartas (Carta cartaUm, Carta cartaDois)
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

            foreach(Carta c in OutraMao.MelhorJogo)
                if (!MelhorJogo.Contains(c)) return false;

            return true;
        }
    }
}
