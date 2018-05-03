using Enuns;
using System;
using System.Collections.Generic;

namespace Modelo
{
    public class MaoTexasHoldem
    {
        public int IdMeuJogador { get; set; }

    
        IList<Carta> Cartas { get; set; }

        public MaoTexasHoldem(Carta cartaUm, Carta cartaDois)
        {
            Cartas = new List<Carta>()
            {
                cartaUm,
                cartaDois
            };
        }

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
    }
}
