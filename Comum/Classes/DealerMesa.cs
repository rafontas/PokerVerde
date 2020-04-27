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
    public class DealerMesa : IDealerMesa
    {
        public Mesa Mesa { get; }

        public DealerMesa(Mesa mesa, IJogador banca, IJogador jogador)
        {
            this.Mesa = mesa;
            this.dealerPartida = new DealerPartida(this.Mesa, banca);
            this.Mesa.AddParticipante(jogador);
        }

        public DealerMesa(Mesa mesa, IJogador banca, IList<IJogador> jogador)
        {
            this.Mesa = mesa;
            this.dealerPartida = new DealerPartida(this.Mesa, banca);

            foreach(var j in jogador) this.Mesa.AddParticipante(j);
        }

        public DealerMesa(Mesa mesa, IJogador banca)
        {
            this.Mesa = mesa;
            this.dealerPartida = new DealerPartida(this.Mesa, banca);
        }

        private TipoRodada UltimaRodada { get => this.Mesa.PartidasAtuais.First().Value.Rodadas.Last().TipoRodada; }

        private IDealerPartida dealerPartida { get; set; } 

        public void ExecutarCorrida(IJogador jogador)
        {
            throw new NotImplementedException();
        }

        public void ExecutarNovaPartidaCompleta(IJogador j)
        {
            throw new NotImplementedException();
        }

        public void ExecutarProximaRodadaPartidaAtual(IJogador j)
        {
            throw new NotImplementedException();
        }

        public void ExecutaTodasCorridas()
        {

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
            this.dealerPartida.PrepararNovaPartida();
            this.dealerPartida.PergutarQuemIraJogar();
        }

        public void ExecutarProximaRodadaPartidaAtual()
        {
            if (!this.dealerPartida.ExistePartidaEmAndamento()) 
                throw new Exception("Não existe partida em andamento");

            switch (this.UltimaRodada)
            {
                case TipoRodada.PreJogo:
                    this.dealerPartida.ExecutarPreFlop();
                    break;

                case TipoRodada.PreFlop: 
                    this.dealerPartida.PerguntarPagarFlop(); 
                    break;

                case TipoRodada.Flop: 
                    this.dealerPartida.RevelarFlop(); 
                    this.dealerPartida.PerguntarAumentarPreTurn(); 
                    break;

                case TipoRodada.Turn: 
                    this.dealerPartida.RevelarTurn(); 
                    this.dealerPartida.PerguntarAumentarPreRiver(); 
                    break;

                case TipoRodada.River: 
                    this.dealerPartida.RevelarRiver(); 
                    this.dealerPartida.EncerrarPartidas(); 
                    break;

                default: throw new DealerException("Ultima rodada não encontrada.");
            }
        }

        public void TerminarPartidaAtual()
        {
            while (this.dealerPartida.ExistePartidaEmAndamento())
                this.ExecutarProximaRodadaPartidaAtual();
        }

        public void ExecutarNovaPartidaCompleta()
        {
            this.IniciarNovaPartida();
            this.TerminarPartidaAtual();
        }

        public void ExecutarCorrida()
        {
            while (this.Mesa.ParticipantesJogando.First().VouJogarMaisUmaPartida())
                this.ExecutarNovaPartidaCompleta();
        }

        public bool HaParticipantesParaJogar() => this.dealerPartida.HaJogadoresParaJogar();

    }
}
