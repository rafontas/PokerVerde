using Comum.Classes;
using Comum.Interfaces;
using JogadorTH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Testes.Modelos;

namespace Testes
{
    [TestClass]
    public class MesaTest
    {
        static ConfiguracaoTHBonus configPadrao { get => Configuracao.configPadrao; }

        [TestMethod]
        public void HaJogadoresParaJogar_1()
        {
            IJogador j = new DummyJogadorTHB(MesaTest.configPadrao);
            Comum.Mesa m = new Comum.Mesa(MesaTest.configPadrao);
            IDealerPartida d = new Comum.Classes.DealerPartida(m);

            Assert.AreNotEqual(d.Mesa.Participantes.Count, 1);

            d.Mesa.AddParticipante(j);

            Assert.AreEqual(d.Mesa.Participantes.Count, 1);

        }

    }
}
