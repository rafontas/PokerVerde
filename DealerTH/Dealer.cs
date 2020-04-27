using Modelo;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Comum.Interfaces;
using Comum;

namespace DealerTH
{
    public class Dealer : IDealerMesa
    {
        protected uint pot { get; set; }
        public uint GetPot() => this.pot;
        private IList<IPartida> partidasJogadas { get; set; }
        private IList<IJogador> jogadoresInteressados { get; set; } = new List<IJogador>();
        public IList<IJogador> JogadoresInteressados { get => jogadoresInteressados; private set => jogadoresInteressados = value; }

        public Mesa Mesa => throw new NotImplementedException();

        public IList<IPartida> GetMemoria() => partidasJogadas;

        public IRodada ExecutaProximaRodada()
        {
            throw new NotImplementedException();
        }

        public IPartida ExecutarPartidaCompleta()
        {
            throw new NotImplementedException();
        }

        public IList<IJogador> GetWinner(IList<Carta> Mesa, IList<IJogador> jogadores)
        {
            throw new NotImplementedException();
        }

        public void IniciaNovaPartida()
        {
            throw new NotImplementedException();
        }

        public int JogadorGanhouTHB(IList<Carta> Mesa, Carta[] CartaJogador, Carta[] CartaBanca)
        {
            // Monta a melhor mão do jogador
            MelhorMao jogador = new MelhorMao();
            MaoTexasHoldem MaoJogador = jogador.AvaliaMao(new List<Carta>() { CartaJogador[0], CartaJogador[1],
                Mesa[0], Mesa[1], Mesa[2], Mesa[3], Mesa[4] });

            // Monta a melhor mão da banca
            MelhorMao mesa = new MelhorMao();
            MaoTexasHoldem MaoMesa = mesa.AvaliaMao(new List<Carta>() { CartaBanca[0], CartaBanca[1],
                Mesa[0], Mesa[1], Mesa[2], Mesa[3], Mesa[4] });

            return MaoJogador.Compara(MaoMesa);
        }

        public bool PartidaAtualTerminou()
        {
            throw new NotImplementedException();
        }

        public IPartida TerminarPartida()
        {
            throw new NotImplementedException();
        }

        public void IniciarNovaPartida(IJogador j)
        {
            throw new NotImplementedException();
        }

        public void ExecutarProximaRodadaPartidaAtual(IJogador j)
        {
            throw new NotImplementedException();
        }

        public void TerminarPartidaAtual(IJogador j)
        {
            throw new NotImplementedException();
        }

        public void ExecutarNovaPartidaCompleta(IJogador j)
        {
            throw new NotImplementedException();
        }

        public void ExecutarCorrida(IJogador jogador)
        {
            throw new NotImplementedException();
        }

        public void ExecutaTodasCorridas()
        {
            throw new NotImplementedException();
        }

        public bool HaParticipantesParaJogar()
        {
            throw new NotImplementedException();
        }

        public void IniciarNovaPartida()
        {
            throw new NotImplementedException();
        }

        public void ExecutarProximaRodadaPartidaAtual()
        {
            throw new NotImplementedException();
        }

        public void TerminarPartidaAtual()
        {
            throw new NotImplementedException();
        }

        public void ExecutarNovaPartidaCompleta()
        {
            throw new NotImplementedException();
        }

        public void ExecutarCorrida()
        {
            throw new NotImplementedException();
        }
    }
}
