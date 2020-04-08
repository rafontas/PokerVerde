using DealerTH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System.Collections.Generic;

namespace Testes
{
    [TestClass]
    public class MontaMao
    {
        [TestMethod]
        public void StraightFlush_1()
        {
            MelhorMao m = new DealerTH.MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(7, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(3, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.StraightFlush,
                MelhorJogo = new List<Carta>() {
                            new Carta(5, Enuns.Naipe.Copas),
                            new Carta(4, Enuns.Naipe.Copas),
                            new Carta(3, Enuns.Naipe.Copas),
                            new Carta(2, Enuns.Naipe.Copas),
                            new Carta(1, Enuns.Naipe.Copas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void StraightFlush_2()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(13, Enuns.Naipe.Copas),
                new Carta(12, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(7, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Copas),
                new Carta(11, Enuns.Naipe.Copas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.StraightFlush,
                MelhorJogo = new List<Carta>() {
                            new Carta(14, Enuns.Naipe.Copas),
                            new Carta(13, Enuns.Naipe.Copas),
                            new Carta(12, Enuns.Naipe.Copas),
                            new Carta(11, Enuns.Naipe.Copas),
                            new Carta(10, Enuns.Naipe.Copas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void DuasDuplas_1()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(6, Enuns.Naipe.Espadas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(6, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(3, Enuns.Naipe.Copas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.DuasDuplas,
                MelhorJogo = new List<Carta>() {
                            new Carta(14, Enuns.Naipe.Ouros),
                            new Carta(14, Enuns.Naipe.Copas),
                            new Carta(6, Enuns.Naipe.Espadas),
                            new Carta(6, Enuns.Naipe.Paus),
                            new Carta(4, Enuns.Naipe.Copas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void DuasDuplas_2()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(5, Enuns.Naipe.Ouros),
                new Carta(5, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Espadas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(6, Enuns.Naipe.Paus),
                new Carta(13, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Copas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.DuasDuplas,
                MelhorJogo = new List<Carta>() {
                            new Carta(14, Enuns.Naipe.Espadas),
                            new Carta(14, Enuns.Naipe.Copas),
                            new Carta(5, Enuns.Naipe.Ouros),
                            new Carta(5, Enuns.Naipe.Paus),
                            new Carta(13, Enuns.Naipe.Copas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Trinca_1()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(3, Enuns.Naipe.Espadas),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Espadas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Trinca,
                MelhorJogo = new List<Carta>() {
                            new Carta(10, Enuns.Naipe.Paus),
                            new Carta(10, Enuns.Naipe.Espadas),
                            new Carta(10, Enuns.Naipe.Ouros),
                            new Carta(9, Enuns.Naipe.Paus),
                            new Carta(7, Enuns.Naipe.Copas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Trinca_2()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(13, Enuns.Naipe.Paus),
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(13, Enuns.Naipe.Ouros),
                new Carta(3, Enuns.Naipe.Espadas),
                new Carta(13, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Espadas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Trinca,
                MelhorJogo = new List<Carta>() {
                            new Carta(13, Enuns.Naipe.Paus),
                            new Carta(13, Enuns.Naipe.Copas),
                            new Carta(13, Enuns.Naipe.Ouros),
                            new Carta(14, Enuns.Naipe.Espadas),
                            new Carta(7, Enuns.Naipe.Copas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Nada_1()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(12, Enuns.Naipe.Paus),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(3, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(8, Enuns.Naipe.Copas),
                new Carta(5, Enuns.Naipe.Copas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.CartaAlta,
                MelhorJogo = new List<Carta>() {
                            new Carta(12, Enuns.Naipe.Paus),
                            new Carta(10, Enuns.Naipe.Ouros),
                            new Carta(9, Enuns.Naipe.Paus),
                            new Carta(8, Enuns.Naipe.Copas),
                            new Carta(7, Enuns.Naipe.Copas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Nada_2()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(11, Enuns.Naipe.Espadas),
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(3, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.CartaAlta,
                MelhorJogo = new List<Carta>() {
                            new Carta(14, Enuns.Naipe.Espadas),
                            new Carta(11, Enuns.Naipe.Espadas),
                            new Carta(10, Enuns.Naipe.Ouros),
                            new Carta(9, Enuns.Naipe.Paus),
                            new Carta(4, Enuns.Naipe.Copas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Duplas_1()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(11, Enuns.Naipe.Espadas),
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(3, Enuns.Naipe.Copas),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(3, Enuns.Naipe.Ouros),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Dupla,
                MelhorJogo = new List<Carta>() {
                            new Carta(3, Enuns.Naipe.Ouros),
                            new Carta(3, Enuns.Naipe.Copas),
                            new Carta(14, Enuns.Naipe.Espadas),
                            new Carta(11, Enuns.Naipe.Espadas),
                            new Carta(10, Enuns.Naipe.Ouros),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Duplas_2()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(11, Enuns.Naipe.Espadas),
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(13, Enuns.Naipe.Copas),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(14, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(3, Enuns.Naipe.Ouros),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Dupla,
                MelhorJogo = new List<Carta>() {
                            new Carta(14, Enuns.Naipe.Copas),
                            new Carta(14, Enuns.Naipe.Espadas),
                            new Carta(13, Enuns.Naipe.Copas),
                            new Carta(11, Enuns.Naipe.Espadas),
                            new Carta(10, Enuns.Naipe.Ouros),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void FullHand_1()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(11, Enuns.Naipe.Paus),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(11, Enuns.Naipe.Espadas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Copas),
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Espadas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.FullHand,
                MelhorJogo = new List<Carta>() {
                            new Carta(10, Enuns.Naipe.Copas),
                            new Carta(10, Enuns.Naipe.Paus),
                            new Carta(10, Enuns.Naipe.Espadas),
                            new Carta(11, Enuns.Naipe.Espadas),
                            new Carta(11, Enuns.Naipe.Paus),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void FullHand_2()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(11, Enuns.Naipe.Paus),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(11, Enuns.Naipe.Espadas),
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(10, Enuns.Naipe.Espadas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.FullHand,
                MelhorJogo = new List<Carta>() {
                            new Carta(10, Enuns.Naipe.Copas),
                            new Carta(10, Enuns.Naipe.Paus),
                            new Carta(10, Enuns.Naipe.Espadas),
                            new Carta(14, Enuns.Naipe.Copas),
                            new Carta(14, Enuns.Naipe.Ouros),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Four_1()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(10, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(11, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(10, Enuns.Naipe.Espadas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Four,
                MelhorJogo = new List<Carta>() {
                            new Carta(10, Enuns.Naipe.Copas),
                            new Carta(10, Enuns.Naipe.Paus),
                            new Carta(10, Enuns.Naipe.Espadas),
                            new Carta(10, Enuns.Naipe.Ouros),
                            new Carta(11, Enuns.Naipe.Espadas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Four_2()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(2, Enuns.Naipe.Paus),
                new Carta(2, Enuns.Naipe.Espadas),
                new Carta(2, Enuns.Naipe.Ouros),
                new Carta(13, Enuns.Naipe.Espadas),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Four,
                MelhorJogo = new List<Carta>() {
                            new Carta(2, Enuns.Naipe.Copas),
                            new Carta(2, Enuns.Naipe.Paus),
                            new Carta(2, Enuns.Naipe.Espadas),
                            new Carta(2, Enuns.Naipe.Ouros),
                            new Carta(13, Enuns.Naipe.Espadas),
                        }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Straight_1()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(12, Enuns.Naipe.Espadas),
                new Carta(2, Enuns.Naipe.Espadas),
                new Carta(8, Enuns.Naipe.Espadas),
                new Carta(5, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(9, Enuns.Naipe.Copas),
                new Carta(11, Enuns.Naipe.Paus),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Straight,
                MelhorJogo = new List<Carta>()
                {
                    new Carta(12, Enuns.Naipe.Espadas),
                    new Carta(11, Enuns.Naipe.Paus),
                    new Carta(10, Enuns.Naipe.Paus),
                    new Carta(9, Enuns.Naipe.Copas),
                    new Carta(8, Enuns.Naipe.Espadas),
                }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Straight_2()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(12, Enuns.Naipe.Espadas),
                new Carta(2, Enuns.Naipe.Espadas),
                new Carta(8, Enuns.Naipe.Espadas),
                new Carta(13, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(9, Enuns.Naipe.Copas),
                new Carta(11, Enuns.Naipe.Paus),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Straight,
                MelhorJogo = new List<Carta>()
                {
                    new Carta(13, Enuns.Naipe.Espadas),
                    new Carta(12, Enuns.Naipe.Espadas),
                    new Carta(11, Enuns.Naipe.Paus),
                    new Carta(10, Enuns.Naipe.Paus),
                    new Carta(9, Enuns.Naipe.Copas),
                }
            };

            Assert.AreEqual(esperado, atual);
        }

        [TestMethod]
        public void Straight_3()
        {
            MelhorMao m = new MelhorMao();
            IList<Carta> entrada = new List<Carta>()
            {
                new Carta(12, Enuns.Naipe.Espadas),
                new Carta(2, Enuns.Naipe.Espadas),
                new Carta(14, Enuns.Naipe.Espadas),
                new Carta(13, Enuns.Naipe.Espadas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(9, Enuns.Naipe.Copas),
                new Carta(11, Enuns.Naipe.Paus),
            };

            MaoTexasHoldem atual = m.AvaliaMao(entrada);
            MaoTexasHoldem esperado = new MaoTexasHoldem()
            {
                MeuJogo = Enuns.Jogo.Straight,
                MelhorJogo = new List<Carta>()
                {
                    new Carta(14, Enuns.Naipe.Espadas),
                    new Carta(13, Enuns.Naipe.Espadas),
                    new Carta(12, Enuns.Naipe.Espadas),
                    new Carta(11, Enuns.Naipe.Paus),
                    new Carta(10, Enuns.Naipe.Paus),
                }
            };

            Assert.AreEqual(esperado, atual);
        }
    }
}
