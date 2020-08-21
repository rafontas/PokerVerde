using Comum.Classes;
using Comum.Interfaces;
using JogadorTH;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testes.Modelos.Mocks;

namespace Testes.Factories
{
    internal class PartidaFactory
    {
        private ConfiguracaoTHBonus _configuracaoPadrao { get; set; } = null;
        
        internal ConfiguracaoTHBonus ConfiguracaoPadrao 
        {
            get => this._configuracaoPadrao ?? new ConfiguracaoTHBonus(){ Ant = 5, Flop = 10, Turn = 5, River = 5 };
            set => this._configuracaoPadrao = value;
        }
        
        internal PartidaFactory() { }
        
        internal PartidaFactory(ConfiguracaoTHBonus c) { this._configuracaoPadrao = c; }

        internal IList<IPartida> GetJogadorGanhou(int numPartidasRetornadas, IJogador j = null) 
        {
            IList<IPartida> retorno = new List<IPartida>();

            for (int i = 0; i < numPartidasRetornadas; i++)
                retorno.Add(this.GetJogadorGanhou(j));

            return retorno;
        }

        internal IPartida GetJogadorGanhou(IJogador j = null) 
        {
            uint stackInicialJogador = 20000;
            
            Comum.Mesa m = new Comum.Mesa(this.ConfiguracaoPadrao);
            IJogador banca = new Banca(this.ConfiguracaoPadrao);
            ICroupier dealer = new Croupier(m, banca);
            dealer.DealerPartida = new DealerPartidaMock(m, banca, Enuns.VencedorPartida.Jogador );

            IJogador jogador = j ?? new DummyJogadorTHB(this.ConfiguracaoPadrao, stackInicialJogador);

            m.AddParticipante(jogador);

            dealer.ExecutarNovaPartidaCompleta();

            //while(jogador.Historico.Last().JogadorGanhador != Enuns.VencedorPartida.Jogador)
            //    dealer.ExecutarNovaPartidaCompleta();

            return jogador.Historico.Last();            
        }

        internal IList<IPartida> GetBancaGanhou(int numPartidasRetornadas)
        {
            IList<IPartida> retorno = new List<IPartida>();

            for (int i = 0; i < numPartidasRetornadas; i++)
                retorno.Add(this.GetBancaGanhou());

            return retorno;
        }

        internal IPartida GetBancaGanhou()
        {
            uint stackInicialJogador = 20000;
            Comum.Mesa m = new Comum.Mesa(this.ConfiguracaoPadrao);
            ICroupier dealer = new Croupier(m, new Banca(this.ConfiguracaoPadrao));
            IJogador jogador = new DummyJogadorTHB(this.ConfiguracaoPadrao, stackInicialJogador);
            m.AddParticipante(jogador);
            dealer.ExecutarNovaPartidaCompleta();

            while (dealer.HaParticipantesParaJogar() && jogador.Historico.Last().JogadorGanhador != Enuns.VencedorPartida.Banca)
                dealer.ExecutarNovaPartidaCompleta();

            return jogador.Historico.Last();
        }

        internal IList<IPartida> GetEmpate(int numPartidasRetornadas)
        {
            IList<IPartida> retorno = new List<IPartida>();

            for (int i = 0; i < numPartidasRetornadas; i++)
                retorno.Add(this.GetEmpate());

            return retorno;
        }

        internal IPartida GetEmpate()
        {
            uint stackInicialJogador = 20000;
            Comum.Mesa m = new Comum.Mesa(this.ConfiguracaoPadrao);
            ICroupier dealer = new Croupier(m, new Banca(this.ConfiguracaoPadrao));
            IJogador jogador = new DummyJogadorTHB(this.ConfiguracaoPadrao, stackInicialJogador);
            m.AddParticipante(jogador);
            dealer.ExecutarNovaPartidaCompleta();

            while (dealer.HaParticipantesParaJogar() && jogador.Historico.Last().JogadorGanhador != Enuns.VencedorPartida.Empate)
                dealer.ExecutarNovaPartidaCompleta();

            return jogador.Historico.Last();
        }

        internal IList<IPartida> GetPartidasSortidas(int qtdJogadorGanhou = 0, int qtdBancaGanhou = 0, int qtdEmpate = 0) 
        {
            IList<IPartida> listaFinal = new List<IPartida>();

            if ((qtdJogadorGanhou + qtdBancaGanhou + qtdEmpate) == 0) {
                Random rnd = new Random();
                qtdJogadorGanhou = rnd.Next(1, 100);
                qtdEmpate = rnd.Next(1, 100);
                qtdJogadorGanhou = rnd.Next(1, 100);
            }

            listaFinal = listaFinal.Concat(this.GetJogadorGanhou(qtdJogadorGanhou)).ToList();
            listaFinal = listaFinal.Concat(this.GetBancaGanhou(qtdBancaGanhou)).ToList();
            listaFinal = listaFinal.Concat(this.GetEmpate(qtdEmpate)).ToList();

            return listaFinal;
        }
    }
}
