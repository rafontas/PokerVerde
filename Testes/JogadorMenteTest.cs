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
    public class JogadorMenteTest
    {
        ConfiguracaoTHBonus configPadrao { get => Configuracao.configPadrao; }

        // Testes Básicos
        [TestMethod]
        public void PreJogo()
        {
            uint valorStackInicial = 100;
            IJogador j = new DummyJogadorTHB(this.configPadrao, valorStackInicial);

            AcaoJogador a = j.PreJogo(this.configPadrao.Ant);
            Assert.AreEqual(j.Stack, valorStackInicial);
            Assert.IsInstanceOfType(a, typeof(AcaoJogador));
            Assert.AreEqual(a.Acao, Enuns.AcoesDecisaoJogador.Pay);
        }

        [TestMethod]
        public void VerFlop_Call()
        {
            uint valorStackInicial = 100;
            uint valorStackPago = 100;
            
            IJogador j = new DummyJogadorTHB(this.configPadrao, valorStackInicial);

            AcaoJogador preJogo = j.PreJogo(this.configPadrao.Ant);
            j.PagaValor(this.configPadrao.Ant);
            valorStackPago -= this.configPadrao.Ant;

            Assert.IsTrue(j.Stack == valorStackPago);

            AcaoJogador preFlop = j.PreFlop(this.configPadrao.Flop);
            j.PagaValor(this.configPadrao.Flop);
            valorStackPago -= this.configPadrao.Flop;

            Assert.IsTrue(j.Stack == valorStackPago);
            Assert.AreEqual(preFlop.Acao, Enuns.AcoesDecisaoJogador.Check);
        }

        [TestMethod]
        public void VerTurn_Call()
        {
            uint valorStackInicial = 100;
            uint valorStackPago = 100;
            
            IJogador j = new DummyJogadorTHB(this.configPadrao, valorStackInicial);

            AcaoJogador preJogo = j.PreJogo(this.configPadrao.Ant);
            j.PagaValor(this.configPadrao.Ant);
            valorStackPago -= this.configPadrao.Ant;

            Assert.IsTrue(j.Stack == valorStackPago);

            AcaoJogador preFlop = j.PreFlop(this.configPadrao.Flop);
            j.PagaValor(this.configPadrao.Flop);
            valorStackPago -= this.configPadrao.Flop;

            AcaoJogador turn = j.Flop(null, this.configPadrao.Turn);
            j.PagaValor(this.configPadrao.Turn);
            valorStackPago -= this.configPadrao.Turn;

            Assert.IsTrue(j.Stack == valorStackPago);
            Assert.AreEqual(turn.Acao, Enuns.AcoesDecisaoJogador.Call);
        }

        [TestMethod]
        public void VerTurn_Check()
        {
            uint valorStackInicial = 100;
            uint valorStackPago = 100;
            
            IJogador j = new DummyJogadorTHB(this.configPadrao, valorStackInicial);

            AcaoJogador preJogo = j.PreJogo(this.configPadrao.Ant);
            j.PagaValor(this.configPadrao.Ant);
            valorStackPago -= this.configPadrao.Ant;

            Assert.IsTrue(j.Stack == valorStackPago);

            AcaoJogador preFlop = j.PreFlop(this.configPadrao.Flop);
            j.PagaValor(this.configPadrao.Flop);
            valorStackPago -= this.configPadrao.Flop;

            AcaoJogador turn = j.Flop(null, 0);
            j.PagaValor(0);
            valorStackPago -= 0;

            Assert.IsTrue(j.Stack == valorStackPago);
            Assert.AreEqual(turn.Acao, Enuns.AcoesDecisaoJogador.Check);
        }

        [TestMethod]
        public void VerRiver_Call()
        {
            uint valorStackInicial = 100;
            uint valorStackPago = 100;
            
            IJogador j = new DummyJogadorTHB(this.configPadrao, valorStackInicial);

            // Pre Jogo
            AcaoJogador preJogo = j.PreJogo(this.configPadrao.Ant);
            j.PagaValor(this.configPadrao.Ant);
            valorStackPago -= this.configPadrao.Ant;

            // Ver Flop
            AcaoJogador verFlop = j.PreFlop(this.configPadrao.Flop);
            j.PagaValor(this.configPadrao.Flop);
            valorStackPago -= this.configPadrao.Flop;

            // Ver Turn
            AcaoJogador verTurn = j.Flop(null, 0);

            // Ver River
            AcaoJogador verRiver = j.Turn(null, this.configPadrao.Turn);
            j.PagaValor(this.configPadrao.Turn);
            valorStackPago -= this.configPadrao.Turn;

            // Testa
            Assert.IsTrue(j.Stack == valorStackPago);
            Assert.AreEqual(verRiver.Acao, Enuns.AcoesDecisaoJogador.Call);
        }

        [TestMethod]
        public void VerRiver_Check()
        {
            uint valorStackInicial = 100;
            uint valorStackPago = 100;

            IJogador j = new DummyJogadorTHB(this.configPadrao, valorStackInicial);

            // Pre Jogo
            AcaoJogador preJogo = j.PreJogo(this.configPadrao.Ant);
            j.PagaValor(this.configPadrao.Ant);
            valorStackPago -= this.configPadrao.Ant;

            // Ver Flop
            AcaoJogador verFlop = j.PreFlop(this.configPadrao.Flop);
            j.PagaValor(this.configPadrao.Flop);
            valorStackPago -= this.configPadrao.Flop;

            // Ver Turn
            AcaoJogador verTurn = j.Flop(null, 0);

            // Ver River
            AcaoJogador verRiver = j.Turn(null, 0);

            // Testa
            Assert.IsTrue(j.Stack == valorStackPago);
            Assert.AreEqual(verRiver.Acao, Enuns.AcoesDecisaoJogador.Check);
        }

        [TestMethod]
        public void PreJogoSemStackParaAnt() { }
        public void PreJogoSemStackParaFlop() { }
        public void VerFlopSemStack() { }
        public void VerTurnSemStackParaRaise() { }
        public void VerRiverSemStackParaRaise() { }
    }
}
