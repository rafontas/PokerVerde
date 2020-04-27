using Comum.Classes;
using Comum.Interfaces;
using JogadorTH;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


        internal IPartida GetJogadorGanhou() 
        {
            uint stackInicialJogador = 20000;
            Comum.Mesa m = new Comum.Mesa(this.ConfiguracaoPadrao);
            IDealerMesa dealer = new DealerMesa(m, new Banca(this.ConfiguracaoPadrao));
            IJogador jogador = new DummyJogadorTHB(this.ConfiguracaoPadrao, stackInicialJogador);
            m.AddParticipante(jogador);
            dealer.ExecutarNovaPartidaCompleta();

            while(dealer.HaParticipantesParaJogar() && 
                  jogador.Historico.Last().JogadorGanhador != Enuns.VencedorPartida.Jogador)
                dealer.ExecutarNovaPartidaCompleta();

            return jogador.Historico.Last();            
        }

        internal IPartida GetBancaGanhou()
        {
            uint stackInicialJogador = 20000;
            Comum.Mesa m = new Comum.Mesa(this.ConfiguracaoPadrao);
            IDealerMesa dealer = new DealerMesa(m, new Banca(this.ConfiguracaoPadrao));
            IJogador jogador = new DummyJogadorTHB(this.ConfiguracaoPadrao, stackInicialJogador);
            m.AddParticipante(jogador);
            dealer.ExecutarNovaPartidaCompleta();

            while (dealer.HaParticipantesParaJogar() && jogador.Historico.Last().JogadorGanhador != Enuns.VencedorPartida.Banca)
                dealer.ExecutarNovaPartidaCompleta();

            return jogador.Historico.Last();
        }

        internal IPartida GetEmpate()
        {
            uint stackInicialJogador = 20000;
            Comum.Mesa m = new Comum.Mesa(this.ConfiguracaoPadrao);
            IDealerMesa dealer = new DealerMesa(m, new Banca(this.ConfiguracaoPadrao));
            IJogador jogador = new DummyJogadorTHB(this.ConfiguracaoPadrao, stackInicialJogador);
            m.AddParticipante(jogador);
            dealer.ExecutarNovaPartidaCompleta();

            while (dealer.HaParticipantesParaJogar() && jogador.Historico.Last().JogadorGanhador != Enuns.VencedorPartida.Empate)
                dealer.ExecutarNovaPartidaCompleta();

            return jogador.Historico.Last();
        }
    }
}
