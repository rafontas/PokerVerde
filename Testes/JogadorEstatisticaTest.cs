using Comum.Classes;
using Comum.Interfaces;
using JogadorTH;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testes.Modelos;

namespace Testes
{
    [TestClass]
    public class JogadorEstatisticaTest
    {

        [TestMethod]
        public void getQuantidadeJogosJogadosTest() 
        {
            ConfiguracaoTHBonus config = Configuracao.configPadrao;
            Comum.Mesa m = new Comum.Mesa(config);
            int numeroPartidas = 5;
            IDealerMesa dealer = new DealerMesa(m, new Banca(config));

            IJogador j = new DummyJogadorTHB(config);
            m.AddParticipante(j);

            dealer.ExecutarNovaPartidaCompleta();
            dealer.ExecutarNovaPartidaCompleta();
            dealer.ExecutarNovaPartidaCompleta();
            dealer.ExecutarNovaPartidaCompleta();
            dealer.ExecutarNovaPartidaCompleta();

            Assert.IsTrue(numeroPartidas == j.Estatistica.getQuantidadeJogosJogados());
        }
        
        [TestMethod]
        public void getStackInicialTest()
        {
            ConfiguracaoTHBonus config = Configuracao.configPadrao;
            Comum.Mesa m = new Comum.Mesa(config);
            IDealerMesa dealer = new DealerMesa(m, new Banca(config));

            IJogador j = new DummyJogadorTHB(config, 1500);
            m.AddParticipante(j);
            int stackInicial = 1500;

            dealer.ExecutarNovaPartidaCompleta();

            Assert.IsTrue(stackInicial == j.Estatistica.getStackInicial());
        }

        [TestMethod]
        public void getStackFinalTest()
        {
            ConfiguracaoTHBonus config = Configuracao.configPadrao;
            Comum.Mesa m = new Comum.Mesa(config);
            IDealerMesa dealer = new DealerMesa(m, new Banca(config));

            IJogador j = new DummyJogadorTHB(config, 1500);
            m.AddParticipante(j);

            int stackInicial = 1500, stackFinal = 1500;
            int qtdPerdida = 0, qtdGanho = 0, qtdEmpate = 0, numJogos = 100;

            for (int i = 0; i < numJogos; i++)
            {
                dealer.ExecutarNovaPartidaCompleta();
            }

            qtdPerdida = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Banca).Count();
            qtdGanho = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count();
            qtdEmpate = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Empate).Count();

            stackFinal -= (15 * qtdPerdida);
            stackFinal += (10 * qtdGanho);

            Assert.IsTrue(stackFinal == j.Estatistica.getStackFinal());
        }

        [TestMethod]
        public void getStackSaldoFinalTest()
        {
            ConfiguracaoTHBonus config = Configuracao.configPadrao;
            Comum.Mesa m = new Comum.Mesa(config);
            IDealerMesa dealer = new DealerMesa(m, new Banca(config));

            IJogador j = new DummyJogadorTHB(config, 1500);
            m.AddParticipante(j);

            int stackInicial = 1500, stackFinal = 1500, saldoFinal = 0;
            int qtdPerdida = 0, qtdGanho = 0, qtdEmpate = 0, numJogos = 100;

            for (int i = 0; i < numJogos; i++)
            {
                dealer.ExecutarNovaPartidaCompleta();
            }

            qtdPerdida = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Banca).Count();
            qtdGanho = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count();
            qtdEmpate = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Empate).Count();

            stackFinal -= (15 * qtdPerdida);
            stackFinal += (10 * qtdGanho);
            saldoFinal = (stackFinal - stackInicial);

            Assert.IsTrue(saldoFinal == j.Estatistica.getStackSaldoFinal());
        }
        
        [TestMethod]
        public void getQuantidadeJogosGanhosTest()
        {
            ConfiguracaoTHBonus config = Configuracao.configPadrao;
            Comum.Mesa m = new Comum.Mesa(config);
            IDealerMesa dealer = new DealerMesa(m, new Banca(config));

            IJogador j = new DummyJogadorTHB(config, 1500);
            m.AddParticipante(j);

            int stackInicial = 1500, stackFinal = 1500, saldoFinal = 0;
            int qtdPerdida = 0, qtdGanho = 0, qtdEmpate = 0, numJogos = 100;

            for (int i = 0; i < numJogos; i++)
            {
                dealer.ExecutarNovaPartidaCompleta();
            }

            qtdPerdida = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Banca).Count();
            qtdGanho = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count();

            Assert.IsTrue(qtdGanho == j.Estatistica.getQuantidadeJogosGanhos());
        }
        
        [TestMethod]
        public void getQuantidadeJogosPerdidosTest()
        {
            ConfiguracaoTHBonus config = Configuracao.configPadrao;
            Comum.Mesa m = new Comum.Mesa(config);
            IDealerMesa dealer = new DealerMesa(m, new Banca(config));

            IJogador j = new DummyJogadorTHB(config, 1500);
            m.AddParticipante(j);

            int stackInicial = 1500, stackFinal = 1500, saldoFinal = 0;
            int qtdPerdida = 0, qtdGanho = 0, qtdEmpate = 0, numJogos = 100;

            for (int i = 0; i < numJogos; i++)
            {
                dealer.ExecutarNovaPartidaCompleta();
            }

            qtdPerdida = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Banca).Count();
            qtdGanho = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count();

            Assert.IsTrue(qtdPerdida == j.Estatistica.getQuantidadeJogosPerdidos());
        }

        [TestMethod]
        public void getValorGanhoPorJogoTest()
        {
            ConfiguracaoTHBonus config = Configuracao.configPadrao;
            Comum.Mesa m = new Comum.Mesa(config);
            IDealerMesa dealer = new DealerMesa(m, new Banca(config));

            IJogador j = new DummyJogadorTHB(config, 1500);
            m.AddParticipante(j);

            int stackInicial = 1500, stackFinal = 1500, saldoFinal = 0, valorPorJogo = 0;
            int qtdPerdida = 0, qtdGanho = 0, qtdEmpate = 0, numJogos = 100;

            for (int i = 0; i < numJogos; i++)
            {
                dealer.ExecutarNovaPartidaCompleta();
            }

            qtdPerdida = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Banca).Count();
            qtdGanho = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count();
            qtdEmpate = j.Historico.Where(x => x.JogadorGanhador == Enuns.VencedorPartida.Empate).Count();

            stackFinal -= (15 * qtdPerdida);
            stackFinal += (10 * qtdGanho);
            saldoFinal = (stackFinal - stackInicial);

            valorPorJogo = saldoFinal / numJogos;

            Assert.IsTrue(valorPorJogo == j.Estatistica.getValorGanhoPorJogo());
        }

    }
}
