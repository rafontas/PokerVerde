using Comum.Interfaces;
using MesaTH.Comum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testes
{
    [TestClass]
    public class PartidaTest
    {
        [TestInitialize]
        public void setUp() {

            ConfiguracaoTHBonus c = new ConfiguracaoTHBonus()
            {
                Ant = 5,
                Flop = 10,
                Turn = 5,
                River = 5,
            };
            //IPartida p = new MesaTH.Comum.Partida(c);
        }
        
    }
}
