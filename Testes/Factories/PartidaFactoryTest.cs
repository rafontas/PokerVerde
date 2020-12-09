using Comum.Interfaces;
using JogadorTH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testes.Factories
{
    [TestClass]
    public class PartidaFactoryTest
    {
        internal ConfiguracaoTHBonus ConfiguracaoPadrao
        {
            get => this._configuracaoPadrao ?? new ConfiguracaoTHBonus() { Ant = 5, Flop = 10, Turn = 5, River = 5 };
            set => this._configuracaoPadrao = value;
        }

        private ConfiguracaoTHBonus _configuracaoPadrao { get; set; } = null;

        [TestMethod]
        public void JogadorGanhouTest() 
        {
            int qtdItensDeRetornoUnico = 800;
            int qtdItensNaLista = 137;
            PartidaFactory partidaFactory = new PartidaFactory();

            // TESTE - retorno unitario partida
            for (int i = 0; i < qtdItensDeRetornoUnico; i++) 
                Assert.IsTrue(partidaFactory.GetJogadorGanhou().JogadorGanhador == Enuns.VencedorPartida.Jogador);

            // TESTE - retorno listaf
            IList<IPartida> listaPartida = partidaFactory.GetJogadorGanhou(qtdItensNaLista);

            Assert.IsTrue(listaPartida.Count == qtdItensNaLista);
            Assert.IsTrue(0 == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Banca).Count());
            Assert.IsTrue(0 == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Empate).Count());
            Assert.IsTrue(qtdItensNaLista == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count());

        }

        [TestMethod]
        public void JogadorPassadoParamentroGanhouTest() 
        {
            int qtdItensDeRetornoUnico = 800;
            int qtdItensNaLista = 137;
            uint stackInicialJogador = 20000;

            PartidaFactory partidaFactory = new PartidaFactory();
            IJogador j = new DummyJogadorTHB(this.ConfiguracaoPadrao, stackInicialJogador);
            IJogador j1 = new DummyJogadorTHB(this.ConfiguracaoPadrao, stackInicialJogador);

            // TESTE - retorno unitario partida
            for (int i = 0; i < qtdItensDeRetornoUnico; i++) 
                Assert.IsTrue(partidaFactory.GetJogadorGanhou(j1).JogadorGanhador == Enuns.VencedorPartida.Jogador);

            // TESTE - retorno listaf
            IList<IPartida> listaPartida = partidaFactory.GetJogadorGanhou(qtdItensNaLista, j1);

            Assert.IsTrue(listaPartida.Count == qtdItensNaLista);
            Assert.IsTrue(0 == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Banca).Count());
            Assert.IsTrue(0 == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Empate).Count());
            Assert.IsTrue(qtdItensNaLista == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count());

        }

        [TestMethod]
        public void BancaGanhouTest() 
        {
            int qtdItensDeRetornoUnico = 800;
            int qtdItensNaLista = 15;
            PartidaFactory partidaFactory = new PartidaFactory();

            // TESTE - retorno unitario partida
            for (int i = 0; i < qtdItensDeRetornoUnico; i++)
                Assert.IsTrue(partidaFactory.GetBancaGanhou().JogadorGanhador == Enuns.VencedorPartida.Banca);

            // TESTE - retorno listaf
            IList<IPartida> listaPartida = partidaFactory.GetBancaGanhou(qtdItensNaLista);

            Assert.IsTrue(listaPartida.Count == qtdItensNaLista);
            Assert.IsTrue(qtdItensNaLista == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Banca).Count());
            Assert.IsTrue(0 == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Empate).Count());
            Assert.IsTrue(0 == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count());
        }

        [TestMethod]
        public void EmpateTest() 
        {
            int qtdItensDeRetornoUnico = 800;
            int qtdItensNaLista = 137;
            PartidaFactory partidaFactory = new PartidaFactory();

            // TESTE - retorno unitario partida
            for (int i = 0; i < qtdItensDeRetornoUnico; i++)
                Assert.IsTrue(partidaFactory.GetEmpate().JogadorGanhador == Enuns.VencedorPartida.Empate);

            // TESTE - retorno listaf
            IList<IPartida> listaPartida = partidaFactory.GetEmpate(qtdItensNaLista);

            Assert.IsTrue(listaPartida.Count == qtdItensNaLista);
            Assert.IsTrue(0 == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Banca).Count());
            Assert.IsTrue(qtdItensNaLista == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Empate).Count());
            Assert.IsTrue(0 == listaPartida.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count());
        }

        [TestMethod]
        public void ListaSortidaTest()
        {
            Random rnd = new Random();
            int qtdJogadorGanhou = rnd.Next(40, 350), 
                qtdBancaGanhou = rnd.Next(40, 350), 
                qtdEmpates = rnd.Next(35, 113);

            PartidaFactory partidaSortida = new PartidaFactory();

            IList<IPartida> partidasSortidas = partidaSortida.GetPartidasSortidas(qtdJogadorGanhou, qtdBancaGanhou, qtdEmpates);

            Assert.IsTrue(partidasSortidas.Count == (qtdJogadorGanhou + qtdBancaGanhou + qtdEmpates));
            Assert.IsTrue(qtdBancaGanhou == partidasSortidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Banca).Count());
            Assert.IsTrue(qtdEmpates == partidasSortidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Empate).Count());
            Assert.IsTrue(qtdJogadorGanhou == partidasSortidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count());
        }

    }
}
