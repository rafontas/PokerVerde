using Comum.Classes.Poker.AnaliseProbabilidade;
using Comum.Interfaces.AnaliseProbabilidade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testes
{
    [TestClass]
    public class MaoProbabilidadeTest
    {
        [TestMethod]
        public void Teste1() 
        {
            IList<Carta> mao = new List<Carta>() { 
                new Carta(14, Enuns.Naipe.Copas),
                new Carta(4, Enuns.Naipe.Ouros)
            };

            IMaoProbabilidade m = new MaoProbabilidade(mao);

            string tokenMao = m.ToMaoTokenizada();
            string valorEsperado = "40_A1";

            Assert.IsTrue(tokenMao == valorEsperado);
            Assert.IsTrue(mao[0].CompareTo(new Carta(14, Enuns.Naipe.Ouros)) == -1);
            Assert.IsTrue(mao[0].CompareTo(new Carta(13, Enuns.Naipe.Ouros)) == 1);
            Assert.IsTrue(new Carta(14, Enuns.Naipe.Paus).CompareTo(new Carta(13, Enuns.Naipe.Ouros)) == 1);

            m.AddMesa(new Carta(2, Enuns.Naipe.Espadas));
            valorEsperado = "20_41_A2";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);

            m.AddMesa(new Carta(8, Enuns.Naipe.Espadas));
            valorEsperado = "20_41_80_A2";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);
            
            m.AddMesa(new Carta(13, Enuns.Naipe.Ouros));
            valorEsperado = "20_41_80_K1_A2";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);

            m.AddMesa(new Carta(7, Enuns.Naipe.Paus));
            valorEsperado = "20_41_72_80_K1_A3";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);

            m.AddMesa(new Carta(8, Enuns.Naipe.Paus));
            valorEsperado = "20_41_72_80_82_K1_A3";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);
        }        
        
        [TestMethod]
        public void Teste2() 
        {
            IList<Carta> mao = new List<Carta>() { 
                new Carta(14, Enuns.Naipe.Ouros),
                new Carta(4, Enuns.Naipe.Ouros)
            };

            IMaoProbabilidade m = new MaoProbabilidade(mao);

            string tokenMao = m.ToMaoTokenizada();
            string valorEsperado = "40_A0";
            Assert.IsTrue(tokenMao == valorEsperado);

            m.AddMesa(new Carta(4, Enuns.Naipe.Espadas));
            valorEsperado = "40_41_A0";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);

            m.AddMesa(new Carta(4, Enuns.Naipe.Copas));
            valorEsperado = "40_41_42_A1";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);

            m.AddMesa(new Carta(4, Enuns.Naipe.Paus));
            valorEsperado = "40_41_42_43_A1";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);

            m.AddMesa(new Carta(14, Enuns.Naipe.Copas));
            valorEsperado = "40_41_42_43_A0_A1";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);

            m.AddMesa(new Carta(14, Enuns.Naipe.Paus));
            valorEsperado = "40_41_42_43_A0_A1_A3";
            tokenMao = m.ToMaoTokenizada();
            Assert.IsTrue(tokenMao == valorEsperado);
        }
    }
}
