using Comum.Excecoes;
using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comum.Classes
{
    public class Croupier : ICroupier
    {
        public Mesa Mesa { get; }

        public IDealerPartida DealerPartida { get; set; }

        //todo: retirar este construtor
        public Croupier(Mesa mesa, IJogador banca, IJogador jogador)
        {
            this.Mesa = mesa;
            this.DealerPartida = new DealerPartida(this.Mesa, banca);
            this.Mesa.AddParticipante(jogador);
        }

        public Croupier(Mesa mesa, IJogador banca)
        {
            this.Mesa = mesa;
            this.DealerPartida = new DealerPartida(this.Mesa, banca);
        }
        
        public Croupier(IConfiguracaoPoker config, IJogador banca, IList<IJogador> jogador)
        {
            this.Mesa = new Mesa(config);
            this.DealerPartida = new DealerPartida(this.Mesa, banca);
            foreach (var j in jogador) this.Mesa.AddParticipante(j);
        }

        private TipoRodada UltimaRodada { get => this.Mesa.PartidasAtuais.First().Value.Rodadas.Last().TipoRodada; }

        public void ExecutarCorrida(IJogador jogador)
        {
            throw new NotImplementedException();
        }

        public void ExecutarNovaPartidaCompleta(IJogador jogador)
        {
            throw new NotImplementedException();
        }

        public void ExecutarProximaRodadaPartidaAtual(IJogador j)
        {
            throw new NotImplementedException();
        }

        public void ExecutaTodasCorridas()
        {
            while(this.HaParticipantesParaJogar())
                this.ExecutarNovaPartidaCompleta();
        }

        public void IniciarNovaPartida(IJogador j)
        {
            
        }

        public void TerminarPartidaAtual(IJogador j)

        {
            throw new NotImplementedException();
        }

        public void IniciarNovaPartida()
        {
            this.DealerPartida.PrepararNovaPartida();
            this.DealerPartida.PergutarQuemIraJogar();
        }

        public void ExecutarProximaRodadaPartidaAtual()
        {
            if (!this.DealerPartida.ExistePartidaEmAndamento()) 
                throw new Exception("Não existe partida em andamento");

            switch (this.UltimaRodada)
            {
                case TipoRodada.PreJogo:
                    this.DealerPartida.ExecutarPreFlop();
                    break;

                case TipoRodada.PreFlop: 
                    this.DealerPartida.PerguntarPagarFlop(); 
                    break;

                case TipoRodada.Flop: 
                    this.DealerPartida.RevelarFlop(); 
                    this.DealerPartida.PerguntarAumentarPreTurn(); 
                    break;

                case TipoRodada.Turn: 
                    this.DealerPartida.RevelarTurn(); 
                    this.DealerPartida.PerguntarAumentarPreRiver(); 
                    break;

                case TipoRodada.River: 
                    this.DealerPartida.RevelarRiver(); 
                    this.DealerPartida.EncerrarPartidas(); 
                    break;

                default: 
                    throw new DealerException("Ultima rodada não encontrada.");
            }
        }

        public void TerminarPartidaAtual()
        {
            while (this.DealerPartida.ExistePartidaEmAndamento())
                this.ExecutarProximaRodadaPartidaAtual();
        }

        public void ExecutarNovaPartidaCompleta()
        {
            this.IniciarNovaPartida();
            this.TerminarPartidaAtual();
        }

        public void ExecutarCorrida() => throw new NotImplementedException("Execução de corrida não implementada.");

        public bool HaParticipantesParaJogar() => this.DealerPartida.HaJogadoresParaJogar();

    }
}
