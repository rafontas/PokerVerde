using DealerTH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System.Collections.Generic;

namespace Testes
{
    [TestClass]
    public class TestaMaoChecagem
    {
        [TestMethod]
        public void StraightFlush()
        {
            Carta [] mesaJogador = new Carta[] {
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(3, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),

                new Carta(7, Enuns.Naipe.Paus),
                new Carta(5, Enuns.Naipe.Copas),
            };
            Carta[] mesaBanca = new Carta[] {
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(3, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),

                new Carta(6, Enuns.Naipe.Paus),
                new Carta(6, Enuns.Naipe.Espadas),
            };

            Carta[] cartasMesa  = new Carta[] {
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(3, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(14, Enuns.Naipe.Copas),
            };
            Carta [] cartasBanca = new Carta[] {
                new Carta(6, Enuns.Naipe.Paus),
                new Carta(6, Enuns.Naipe.Espadas),
            };
            Carta [] cartasJogador = new Carta[] {
                new Carta(7, Enuns.Naipe.Paus),
                new Carta(5, Enuns.Naipe.Copas),
            };

            Dealer d = new Dealer();
            d.JogadorGanhouTHB(cartasMesa, cartasJogador, cartasBanca);
            MaoChecagem m = DadosTeste.IniciaMaoChecagem(1, mesaJogador);

            // NUMEROS
            for (int i = 0; i < d.MaoChecagemJogador.Numeros.Length; i++)
                CollectionAssert.AreEquivalent(m.Numeros[i], d.MaoChecagemJogador.Numeros[i]);
            CollectionAssert.AreEquivalent(m.indiceNaipes, d.MaoChecagemJogador.indiceNaipes);

            // NAIPES
            for (int i = 0; i < d.MaoChecagemJogador.Naipes.Length; i++)
                CollectionAssert.AreEquivalent(m.Naipes[i], d.MaoChecagemJogador.Naipes[i]);
            CollectionAssert.AreEquivalent(m.indiceNumeros, d.MaoChecagemJogador.indiceNumeros);

            // Assert do que tem na mão foi encontrado
            Assert.IsTrue(d.MaoChecagemJogador.Dupla);
            Assert.IsFalse(d.MaoChecagemJogador.DuasDuplas);
            Assert.IsFalse(d.MaoChecagemJogador.Trinca);
            Assert.IsTrue(d.MaoChecagemJogador.Straight);
            Assert.IsTrue(d.MaoChecagemJogador.Flush);
            Assert.IsFalse(d.MaoChecagemJogador.Four);
            Assert.IsTrue(d.MaoChecagemJogador.StraightFlush);
        }

        [TestMethod]
        public void DuasDuplas()
        {
            Carta[] mesaBanca = new Carta[] {
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(3, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),

                new Carta(6, Enuns.Naipe.Paus),
                new Carta(6, Enuns.Naipe.Espadas),
            };

            Carta[] cartasJogador = new Carta[] {
                new Carta(7, Enuns.Naipe.Paus),
                new Carta(5, Enuns.Naipe.Copas),
            };
            Carta[] cartasMesa = new Carta[] {
                new Carta(2, Enuns.Naipe.Copas),
                new Carta(3, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Copas),
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(14, Enuns.Naipe.Copas),
            };
            Carta[] cartasBanca = new Carta[] {
                new Carta(6, Enuns.Naipe.Paus),
                new Carta(6, Enuns.Naipe.Espadas),
            };

            Dealer d = new Dealer();
            d.JogadorGanhouTHB(cartasMesa, cartasJogador, cartasBanca);
            MaoChecagem m = DadosTeste.IniciaMaoChecagem(2, mesaBanca);

            // NUMEROS
            for (int i = 0; i < d.MaoChecagemMesa.Numeros.Length; i++)
                CollectionAssert.AreEquivalent(m.Numeros[i], d.MaoChecagemMesa.Numeros[i]);
            CollectionAssert.AreEquivalent(m.indiceNaipes, d.MaoChecagemMesa.indiceNaipes);

            // NAIPES
            for (int i = 0; i < d.MaoChecagemMesa.Naipes.Length; i++)
                CollectionAssert.AreEquivalent(m.Naipes[i], d.MaoChecagemMesa.Naipes[i]);
            CollectionAssert.AreEquivalent(m.indiceNumeros, d.MaoChecagemMesa.indiceNumeros);

            // Assert do que tem na mão foi encontrado
            Assert.IsTrue(d.MaoChecagemMesa.Dupla);
            Assert.IsTrue(d.MaoChecagemMesa.DuasDuplas);
            Assert.IsFalse(d.MaoChecagemMesa.Trinca);
            Assert.IsFalse(d.MaoChecagemMesa.Straight);
            Assert.IsFalse(d.MaoChecagemMesa.Flush);
            Assert.IsFalse(d.MaoChecagemMesa.Four);
            Assert.IsFalse(d.MaoChecagemMesa.StraightFlush);
        }

        [TestMethod]
        public void Trinca()
        {
            Carta[] mesaBanca = new Carta[] {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(3, Enuns.Naipe.Espadas),
            };

            Carta[] cartasJogador = new Carta[] {
                new Carta(12, Enuns.Naipe.Paus),
                new Carta(8, Enuns.Naipe.Copas),
            };
            Carta[] cartasMesa = new Carta[] {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(3, Enuns.Naipe.Espadas),
            };
            Carta[] cartasBanca = new Carta[] {
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(10, Enuns.Naipe.Espadas),
            };

            Dealer d = new Dealer();
            d.JogadorGanhouTHB(cartasMesa, cartasJogador, cartasBanca);
            MaoChecagem m = DadosTeste.IniciaMaoChecagem(3, mesaBanca);

            // NUMEROS
            for (int i = 0; i < d.MaoChecagemMesa.Numeros.Length; i++)
                CollectionAssert.AreEquivalent(m.Numeros[i], d.MaoChecagemMesa.Numeros[i]);
            CollectionAssert.AreEquivalent(m.indiceNaipes, d.MaoChecagemMesa.indiceNaipes);

            // NAIPES
            for (int i = 0; i < d.MaoChecagemMesa.Naipes.Length; i++)
                CollectionAssert.AreEquivalent(m.Naipes[i], d.MaoChecagemMesa.Naipes[i]);
            CollectionAssert.AreEquivalent(m.indiceNumeros, d.MaoChecagemMesa.indiceNumeros);

            // Assert do que tem na mão foi encontrado
            Assert.IsTrue(d.MaoChecagemMesa.Dupla);
            Assert.IsFalse(d.MaoChecagemMesa.DuasDuplas);
            Assert.IsTrue(d.MaoChecagemMesa.Trinca);
            Assert.IsFalse(d.MaoChecagemMesa.Straight);
            Assert.IsFalse(d.MaoChecagemMesa.Flush);
            Assert.IsFalse(d.MaoChecagemMesa.Four);
            Assert.IsFalse(d.MaoChecagemMesa.StraightFlush);
        }

        [TestMethod]
        public void Nada()
        {
            Carta[] mesaJogador = new Carta[] {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(3, Enuns.Naipe.Espadas),
                new Carta(12, Enuns.Naipe.Paus),
                new Carta(8, Enuns.Naipe.Copas),
            };

            Carta[] cartasJogador = new Carta[] {
                new Carta(12, Enuns.Naipe.Paus),
                new Carta(8, Enuns.Naipe.Copas),
            };
            Carta[] cartasMesa = new Carta[] {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Ouros),
                new Carta(9, Enuns.Naipe.Paus),
                new Carta(3, Enuns.Naipe.Espadas),
            };
            Carta[] cartasBanca = new Carta[] {
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(10, Enuns.Naipe.Espadas),
            };

            Dealer d = new Dealer();
            d.JogadorGanhouTHB(cartasMesa, cartasJogador, cartasBanca);
            MaoChecagem m = DadosTeste.IniciaMaoChecagem(4, mesaJogador);

            // NUMEROS
            for (int i = 0; i < d.MaoChecagemJogador.Numeros.Length; i++)
                CollectionAssert.AreEquivalent(m.Numeros[i], d.MaoChecagemJogador.Numeros[i]);
            CollectionAssert.AreEquivalent(m.indiceNaipes, d.MaoChecagemJogador.indiceNaipes);

            // NAIPES
            for (int i = 0; i < d.MaoChecagemJogador.Naipes.Length; i++)
                CollectionAssert.AreEquivalent(m.Naipes[i], d.MaoChecagemJogador.Naipes[i]);
            CollectionAssert.AreEquivalent(m.indiceNumeros, d.MaoChecagemJogador.indiceNumeros);

            // Assert do que tem na mão foi encontrado
            Assert.IsFalse(d.MaoChecagemJogador.Dupla);
            Assert.IsFalse(d.MaoChecagemJogador.DuasDuplas);
            Assert.IsFalse(d.MaoChecagemJogador.Trinca);
            Assert.IsFalse(d.MaoChecagemJogador.Straight);
            Assert.IsFalse(d.MaoChecagemJogador.Flush);
            Assert.IsFalse(d.MaoChecagemJogador.Four);
            Assert.IsFalse(d.MaoChecagemJogador.StraightFlush);
        }

        [TestMethod]
        public void FullHand()
        {
            Carta[] mesaBanca = new Carta[] {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(10, Enuns.Naipe.Espadas),
                new Carta(11, Enuns.Naipe.Paus),
                new Carta(11, Enuns.Naipe.Espadas),
            };

            Carta[] cartasJogador = new Carta[] {
                new Carta(11, Enuns.Naipe.Ouros),
                new Carta(5, Enuns.Naipe.Espadas),
            };
            Carta[] cartasBanca = new Carta[] {
                new Carta(10, Enuns.Naipe.Paus),
                new Carta(10, Enuns.Naipe.Espadas),
            };
            Carta[] cartasMesa = new Carta[] {
                new Carta(5, Enuns.Naipe.Copas),
                new Carta(7, Enuns.Naipe.Copas),
                new Carta(10, Enuns.Naipe.Copas),
                new Carta(11, Enuns.Naipe.Paus),
                new Carta(11, Enuns.Naipe.Espadas),
            };
        
            Dealer d = new Dealer();
            d.JogadorGanhouTHB(cartasMesa, cartasJogador, cartasBanca);
            MaoChecagem m = DadosTeste.IniciaMaoChecagem(5, mesaBanca);

            // NUMEROS
            for (int i = 0; i < d.MaoChecagemJogador.Numeros.Length; i++)
                CollectionAssert.AreEquivalent(m.Numeros[i], d.MaoChecagemMesa.Numeros[i]);
            CollectionAssert.AreEquivalent(m.indiceNaipes, d.MaoChecagemMesa.indiceNaipes);

            // NAIPES
            for (int i = 0; i < d.MaoChecagemMesa.Naipes.Length; i++)
                CollectionAssert.AreEquivalent(m.Naipes[i], d.MaoChecagemMesa.Naipes[i]);
            CollectionAssert.AreEquivalent(m.indiceNumeros, d.MaoChecagemMesa.indiceNumeros);

            // Assert do que tem na mão foi encontrado
            Assert.IsTrue(d.MaoChecagemMesa.Dupla);
            Assert.IsTrue(d.MaoChecagemMesa.DuasDuplas);
            Assert.IsTrue(d.MaoChecagemMesa.Trinca);
            Assert.IsFalse(d.MaoChecagemMesa.Straight);
            Assert.IsFalse(d.MaoChecagemMesa.Flush);
            Assert.IsFalse(d.MaoChecagemMesa.Four);
            Assert.IsFalse(d.MaoChecagemMesa.StraightFlush);
        }

    }
}
