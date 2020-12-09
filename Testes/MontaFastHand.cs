using DealerTH;
using Comum.HoldemHand;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System.Collections.Generic;

namespace Testes
{
    [TestClass]
    public class MontaFastHand
    {
        [TestMethod]
        public void StraightFlush_1()
        {
            string hand1 = string.Empty, hand2 = string.Empty, board = string.Empty;

            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Copas),
            };

            for (int i = 0; i < 5; i++)
                board += entrada[i].ToFastCard() + " ";

            hand1 = "3h 2h";
            hand2 = "6h 8h";


            Hand h1 = new Hand(hand1, board);
            Hand h2 = new Hand(hand2, board);

            Assert.IsTrue(h1 < h2);
        }

        [TestMethod]
        public void StraightFlush_2()
        {
            string hand1 = string.Empty, hand2 = string.Empty, board = string.Empty;

            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(6, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Copas),
            };

            for (int i = 0; i < 5; i++)
                board += entrada[i].ToFastCard() + " ";

            hand1 = "3h 2h";
            hand2 = "7h 8h";


            Hand h1 = new Hand(hand1, board);
            Hand h2 = new Hand(hand2, board);

            Assert.IsTrue(h1 < h2);
        }

        [TestMethod]
        public void DuasDuplas_1()
        {
            string hand1 = string.Empty, hand2 = string.Empty, board = string.Empty;

            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(6, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(14, Enuns.Naipe.Copas),
            };

            for (int i = 0; i < 5; i++)
                board += entrada[i].ToFastCard() + " ";

            hand1 = "5c 6h";
            hand2 = "as 8h";


            Hand h1 = new Hand(hand1, board);
            Hand h2 = new Hand(hand2, board);

            Assert.IsTrue(h1 < h2);
        }

        [TestMethod]
        public void DuasDuplas_2()
        {
            string hand1 = string.Empty, hand2 = string.Empty, board = string.Empty;

            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(6, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(4, Enuns.Naipe.Ouros),
                new Carta(14, Enuns.Naipe.Copas),
            };

            for (int i = 0; i < 5; i++)
                board += entrada[i].ToFastCard() + " ";

            hand1 = "5c 6h";
            hand2 = "as 8h";


            Hand h1 = new Hand(hand1, board);
            Hand h2 = new Hand(hand2, board);

            Assert.IsTrue(h1 > h2);
        }


        [TestMethod]
        public void Dupla_Kicker()
        {
            string hand1 = string.Empty, hand2 = string.Empty, board = string.Empty;

            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(4, Enuns.Naipe.Ouros),
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(6, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Copas),
            };

            for (int i = 0; i < 5; i++)
                board += entrada[i].ToFastCard() + " ";

            hand1 = "5c 9h";
            hand2 = "5s 8h";


            Hand h1 = new Hand(hand1, board);
            Hand h2 = new Hand(hand2, board);

            Assert.IsTrue(h1 > h2);
        }

        [TestMethod]
        public void Dupla_empate()
        {
            string hand1 = string.Empty, hand2 = string.Empty, board = string.Empty;

            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(4, Enuns.Naipe.Ouros),
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(6, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Copas),
            };

            for (int i = 0; i < 5; i++)
                board += entrada[i].ToFastCard() + " ";

            hand1 = "5c 8c";
            hand2 = "5s 8h";


            Hand h1 = new Hand(hand1, board);
            Hand h2 = new Hand(hand2, board);

            Assert.IsTrue(h1 == h2);
        }
    }
}
