using Modelo;
using Modelo.Interfaces;
using System;
using System.Collections.Generic;

namespace Mesa
{
    public class MesaTexasHoldem
    {
        public Deck Deck { get; set; }

        public TipoPoker TipoPoker { get; private set; }

        public IList<IJogador> Jogadores { get; set; }

        /// <summary>
        /// Inicia uma nova mesa com novos jogadores e um novo deck.
        /// </summary>
        /// <param name="numJogadores"></param>
        /// <param name="tipoPoker"></param>
        public MesaTexasHoldem(int numJogadores, TipoPoker tipoPoker = TipoPoker.TexasHoldem)
        {
            Deck = new Deck();
            this.TipoPoker = tipoPoker;
        }

        /// <summary>
        /// Distribui as cartas aos jogadores.
        /// </summary>        
        public void DistribuiCartas()
        {

        }

        /// <summary>
        /// Verifica quais jogadores ainda jogarão.
        /// </summary>
        public void PreJogo() { }

        /// <summary>
        /// Atualiza as meta informações do jogo.
        /// </summary>
        public void PreFlop() { }

        /// <summary>
        /// Inicia depois da distribuição das caraas do flop. Realiza as apostas antes do river.
        /// </summary>
        public void PreRiver() { }

        /// <summary>
        /// Inicia depois da distribuição das cartas do river. Realiza as apostas antes do turn.
        /// </summary>
        public void PreTurn() { }

        /// <summary>
        /// Realiza a última rodada de apostas
        /// </summary>
        public void Turn() { }

    }
}
