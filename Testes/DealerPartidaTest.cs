using JogadorTH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using Comum;
using System;
using System.Collections.Generic;
using System.Text;
using Comum.Classes;
using Comum.Interfaces;

namespace Testes
{
    [TestClass]
    public class DealerPartidaTest
    {
        static ConfiguracaoTHBonus configPadrao
        {
            get
            {
                return new ConfiguracaoTHBonus()
                {
                    Ant = 5,
                    Flop = 10,
                    Turn = 5,
                    River = 5,
                };
            }
        }

        [TestMethod]
        public void HaJogadoresParaJogar_1()
        {
            IJogador j = new DummyJogadorTHB(DealerPartidaTest.configPadrao);
            IJogador banca = new Banca(DealerPartidaTest.configPadrao);
            Comum.Mesa m = new Comum.Mesa(DealerPartidaTest.configPadrao);
            IDealerPartida d = new DealerPartida(m, banca);

            Assert.AreNotEqual(d.Mesa.Participantes.Count, 1);

            d.Mesa.AddParticipante(j);

            Assert.AreEqual(d.Mesa.Participantes.Count, 1);

        }


    }
}
