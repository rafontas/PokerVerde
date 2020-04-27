using Comum.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testes.Factories
{
    [TestClass]
    public class PartidaFactoryTest
    {

        [TestMethod]
        public void PartidaJogadorGanhou() 
        {
            int numeroDeTeste = 100;
            PartidaFactory partidaFactory = new PartidaFactory();

            for (int i = 0; i < numeroDeTeste; i++) 
            {
                Assert.IsTrue(partidaFactory.GetJogadorGanhou().JogadorGanhador == Enuns.VencedorPartida.Jogador);
            }
        }

        [TestMethod]
        public void PartidaBancaGanhou() 
        {
            int numeroDeTeste = 100;
            PartidaFactory partidaFactory = new PartidaFactory();

            for (int i = 0; i < numeroDeTeste; i++)
            {
                Assert.IsTrue(partidaFactory.GetBancaGanhou().JogadorGanhador == Enuns.VencedorPartida.Banca);
            }
        }

        [TestMethod]
        public void PartidaEmpate() 
        {
            int numeroDeTeste = 100;
            PartidaFactory partidaFactory = new PartidaFactory();

            for (int i = 0; i < numeroDeTeste; i++)
            {
                Assert.IsTrue(partidaFactory.GetEmpate().JogadorGanhador == Enuns.VencedorPartida.Empate);
            }
        }

    }
}
