using Comum.Classes;
using Comum.Excecoes;
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
    public class JogadorBaseTest
    {
        ConfiguracaoTHBonus configPadrao { get => Configuracao.configPadrao; }

        [TestMethod]
        public void PagarReceberValorTest() { 

            uint valorStackInicial = 150;
            uint valorPago;
            uint valorRecebido;
            IJogador j = new DummyJogadorTHB(this.configPadrao, valorStackInicial);

            // Pagamento Normal
            valorPago = 50;
            j.PagarValor(valorPago);
            Assert.IsTrue(j.Stack == 100);

            // Pagamento maior do que o que tem
            // Stack jogador = 100
            valorPago = 150;
            Assert.ThrowsException<JogadorException>(() => j.PagarValor(valorPago));

            // Pagamento para zero
            // Stack = 100
            valorPago = 100;
            j.PagarValor(valorPago);
            Assert.IsTrue(j.Stack == 0);

            // Stack = 0
            valorRecebido = 75;
            j.ReceberValor(valorRecebido);
            Assert.IsTrue(75 == j.Stack);
        }

        [TestMethod]
        public void RecebeCartasTest() {
            IJogador j = new DummyJogadorTHB(this.configPadrao, 100);
            
            j.ReceberCarta(
                new Carta(9, Enuns.Naipe.Ouros),
                new Carta(10, Enuns.Naipe.Copas)
            );

            Assert.IsTrue(j.Cartas.Length == 2);
            
            Assert.IsTrue(j.Cartas[0].Naipe == Enuns.Naipe.Ouros && j.Cartas[0].Numero == 9);
            Assert.IsTrue(j.Cartas[1].Naipe == Enuns.Naipe.Copas && j.Cartas[1].Numero == 10);

        }

        [TestMethod]
        public void ResetaMaoTest()
        {
            IJogador j = new DummyJogadorTHB(this.configPadrao, 100);

            j.ReceberCarta(
                new Carta(9, Enuns.Naipe.Ouros),
                new Carta(10, Enuns.Naipe.Copas)
            );

            j.ResetaMao();

            Assert.IsNull(j.Cartas[0]);
            Assert.IsNull(j.Cartas[1]);
            
        }

        [TestMethod]
        public void AddHistoricoTest() 
        {
            IJogador j = new DummyJogadorTHB(this.configPadrao, 100);
            
            Assert.IsTrue(j.Historico.Count == 0);

            j.AddPartidaHistorico(new Partida(1, j, null));

            Assert.IsTrue(j.Historico.Count == 1);

        }

        public void ExecutaAcaoTest() { }

        public void SequencialProximaPartidaTest() { }
    }
}
