using Comum.Classes;
using Comum.Excecoes;
using Comum.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testes
{
    [TestClass]
    public class JogadorStackTest
    {
        [TestMethod]
        public void PagarValorTest() 
        {
            uint stackInicial = 500, valorHaPagar = 50;
            IJogadorStack jogadorStack = new JogadorStack(stackInicial);
            
            jogadorStack.PagarValor(valorHaPagar); // 450 = 500 - 50
            jogadorStack.PagarValor(valorHaPagar); // 400 = 450 - 50

            Assert.IsTrue(jogadorStack.Stack == 400);
            Assert.IsTrue(jogadorStack.StackInicial == stackInicial);
            
            jogadorStack.PagarValor(valorHaPagar * 4); // 200 = 400 - (4 * 50)
            Assert.IsTrue(jogadorStack.Stack == 200);

            // 200 = 200 - 250 > Exceção
            Assert.ThrowsException<JogadorException>(() => jogadorStack.PagarValor(stackInicial));
            Assert.IsTrue(jogadorStack.StackInicial == stackInicial);
            Assert.IsTrue(jogadorStack.Stack == 200);

            jogadorStack.ReceberValor(valorHaPagar * 4); // 400 = 200 + (4 * 50)
            Assert.IsTrue(jogadorStack.Stack == 400);

            Assert.ThrowsException<JogadorException>(() => jogadorStack.PagarValor(stackInicial * 2));
            Assert.IsTrue(jogadorStack.StackInicial == stackInicial);
            Assert.IsTrue(jogadorStack.Stack == 400);
        }
    }
}
