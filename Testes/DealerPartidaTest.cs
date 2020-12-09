using JogadorTH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using Comum;
using System;
using System.Collections.Generic;
using System.Text;
using Comum.Classes;
using Comum.Interfaces;
using Comum.Classes.Poker;
using System.Linq;

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

            Assert.AreNotEqual(d.Mesa.JogadoresNaMesa.Count, 1);

            d.Mesa.AddParticipante(j);

            Assert.AreEqual(d.Mesa.JogadoresNaMesa.Count, 1);

        }

        [TestMethod]
        public void CartasMandatorias_1()
        {
            IJogador jogador = new DummyJogadorTHB(DealerPartidaTest.configPadrao);

            Carta[] cartasJogador = new Carta[] {
                    new Carta(10, Enuns.Naipe.Copas),
                    new Carta(10, Enuns.Naipe.Paus)
            };

            IJogador banca = new Banca(DealerPartidaTest.configPadrao);
            Comum.Mesa m = new Comum.Mesa(DealerPartidaTest.configPadrao);
            int quantidadeJogos = 5;
            
            CroupierConstructParam param = new CroupierConstructParam() {
                CartasJogador = cartasJogador, 
                Jogador = jogador,
                Banca = banca,
                ConfiguracaoPoker = DealerPartidaTest.configPadrao
            };
            ICroupier croupier = new Croupier(param);


            for (int i = 0; i < quantidadeJogos; i++)
            {
                croupier.ExecutarNovaPartidaCompleta();
                IPartida p = jogador.Historico.Last();
                Assert.IsTrue(p.Jogador.Cartas.Contains(cartasJogador[0]) && p.Jogador.Cartas.Contains(cartasJogador[1]));
            }
        }
    }
}
