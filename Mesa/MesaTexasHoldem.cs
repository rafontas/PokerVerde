﻿using Enuns;
using MesaTh;
using MesaTh.Interfaces;
using MesaTH;
using Modelo;
using System;
using System.Collections.Generic;

namespace Mesa
{
    public class MesaTexasHoldem
    {
        public ConfiguracaoTHBonus ConfigMesa { get; set; }

        private InfoMesa infoMesa { get; set; } = new InfoMesa();
        public MomentoJogoControle Momento { get; set; }
        private int numRodada { get; set; } = 1;

        public IJogador Jogador { get; set; }
        public IDealer Dealer { get; set; }

        public Deck<Carta> Deck { get; set; }

        private TipoJogadorTHB UltimoJogadorVencedor { get; set; }

        public IList<Carta> Flop { get; set; } = null;
        public Carta Turn { get; set; } = null;
        public Carta River { get; set; } = null;
        public Carta [] CartasBanca  = new Carta [] { null, null };
        private bool PrecisaAvancarMomento { get; set; } = true;
        
        /// <summary>
        /// Retorna o estado atual da mesa.
        /// </summary>
        /// <returns>Info mesa</returns>
        public string GetInfo() => infoMesa.ToString();

        /// <summary>
        /// Inicia uma nova mesa com novos jogadores e um novo deck.
        /// </summary>
        /// <param name="numJogadores"></param>
        /// <param name="tipoPoker"></param>
        public MesaTexasHoldem(ConfiguracaoTHBonus configuracao,
                                IJogador jogador,
                                IDealer dealer,
                                int NumeroRodadasMaxima = 1)
        {
            ConfigMesa = configuracao;
            Jogador = jogador;
            Dealer = dealer;
            Momento = new MomentoJogoControle();
            PrecisaAvancarMomento = true;

            infoMesa = new InfoMesa()
            {
                ValorAnt = ConfigMesa.Ant,
                ValorFlop = ConfigMesa.Flop,
                ValorTurn = ConfigMesa.Turn,
                ValorRiver = ConfigMesa.River,
                NumMaximoRodadas = NumeroRodadasMaxima,
                Jogador = jogador
            };
        }

        /// <summary>
        /// Reinicia os valores apenas de uma rodada
        /// </summary>
        public void IniciaRodada()
        {
            Momento = new MomentoJogoControle();
            Jogador.ResetaMao();

            Deck = new Deck<Carta>();
            Deck.CriaDeckPadrao();

            infoMesa = new InfoMesa()
            {
                NumRodada = numRodada++,
                ValorAnt = ConfigMesa.Ant,
                ValorFlop = ConfigMesa.Flop,
                ValorTurn = ConfigMesa.Turn,
                ValorRiver = ConfigMesa.River,
                NumMaximoRodadas = infoMesa.NumMaximoRodadas,
                Jogador = Jogador
            };
        }

        /// <summary>
        /// Executa a jogada no modo pausado, sob demanda.
        /// </summary>
        private void ProximoPasso()
        {
            AcaoJogador acao;

            if (Momento.MomentoAtual == TipoRodada.PreJogo)
            {
                PreJogo();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, ConfigMesa.Ant);
            }
            else if (Momento.MomentoAtual == TipoRodada.PreFlop)
            {
                PreFlop();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, ConfigMesa.Flop);
            }
            else if (Momento.MomentoAtual == TipoRodada.Flop)
            {
                DistribuiFlop();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, ConfigMesa.Turn);
            }
            else if (Momento.MomentoAtual == TipoRodada.Turn)
            {
                DistribuiTurn();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, ConfigMesa.River);
            }
            else if (Momento.MomentoAtual == TipoRodada.River)
            {
                DistribuiRiver();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, 0);
                DistribuiCartasMesa();
            }
            else if (Momento.MomentoAtual == TipoRodada.PosRiver)
            {
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, 0);
            }
            else if (Momento.MomentoAtual == TipoRodada.FimDeJogo)
                return;
            else
                throw new Exception("O tipo de jogo requisitado não existe.");

            ManipulaAcaoJogador(acao);
        }

        /// <summary>
        /// Avança o momento de jogo da mesa quanto do jogador
        /// </summary>
        /// <returns></returns>
        public TipoRodada AvancaMomento()
        {
            infoMesa.Momento = Momento.Proximo();
            Jogador.AvancaMomento();
            return infoMesa.Momento;
        }

        /// <summary>
        /// Contabiliza quem venceu o jogo
        /// </summary>
        private void FinalizaJogoContablizaVencedor() 
        {
            switch (Dealer.JogadorGanhouTHB(GetMesa(), Jogador.Cartas, CartasBanca))
            {
                // Empate
                case 0:
                    infoMesa.GanhosMesa += infoMesa.ValorAnt;
                    UltimoJogadorVencedor = TipoJogadorTHB.SemJogador;
                    break;

                // Vitoria
                case 1:
                    Jogador.RecebeValor(infoMesa.GetPote());
                    UltimoJogadorVencedor = TipoJogadorTHB.Jogador;
                    break;

                // Derrota
                case -1:
                    infoMesa.GanhosMesa += infoMesa.GetPote();
                    UltimoJogadorVencedor = TipoJogadorTHB.Mesa;
                    break;
            }

            infoMesa.JogadorGanhador = UltimoJogadorVencedor;
        }

        /// <summary>
        /// Manipula a Ação de um jogador.
        /// </summary>
        /// <param name="acao">A ação tomada pelo jogador.</param>
        /// <returns>O momento data a ação do jogador.</returns>
        private void ManipulaAcaoJogador(AcaoJogador acao)
        {
            this.ValidaAcaoMomentoJogo(acao);

            switch (acao.Acao) 
            {
                case TipoAcao.Fold :
                    Momento.TerminaJogo();
                    infoMesa.Momento = Momento.MomentoAtual;
                    infoMesa.GanhosMesa += infoMesa.GetPote();
                    infoMesa.ReiniciaInfoMesa(TipoJogadorTHB.Mesa);
                    break;

                case TipoAcao.Check:
                    if (TipoRodada.PosRiver == this.Momento.MomentoAtual) this.FinalizaJogoContablizaVencedor();
                    break;

                case TipoAcao.Call: 
                    this.RequisitaPagamentoJogador(acao); 
                    break;
                
                case TipoAcao.Raise: 
                    this.RequisitaPagamentoJogador(acao); 
                    break;

                case TipoAcao.Play: 
                    this.RequisitaPagamentoJogador(acao); 
                    break;

                case TipoAcao.Stop:
                    this.Momento.TerminaJogo(); 
                    break;

                default:
                    throw new MesaException("Erro ao manipular ação do jogador. Tipo de ação não encontrado.");
            }
        }

        /// <summary>
        /// Requisita o pagamento de um jogador de acordo com o momento do jogo.
        /// </summary>
        /// <param name="acao">A ação do jogador</param>
        public void RequisitaPagamentoJogador(AcaoJogador acao)
        {
            if (Momento.MomentoAtual == TipoRodada.PreJogo)
            {
                Jogador.PagaValor(ConfigMesa.Ant);
                infoMesa.ValorInvestidoAnt = ConfigMesa.Ant;
            }
            else if (Momento.MomentoAtual == TipoRodada.PreFlop)
            {
                Jogador.PagaValor(ConfigMesa.Flop);
                infoMesa.ValorInvestidoFlop = ConfigMesa.Flop;
            }
            else if (Momento.MomentoAtual == TipoRodada.Flop)
            {
                Jogador.PagaValor(ConfigMesa.Turn);
                infoMesa.ValorInvestidoTurn = ConfigMesa.Turn;
            }
            else if (Momento.MomentoAtual == TipoRodada.Turn)
            {
                Jogador.PagaValor(ConfigMesa.River);
                infoMesa.ValorInvestidoRiver = ConfigMesa.River;
            }
            else
            {
                throw new Exception("Momento de jogo inválido para pagamento.");
            }
        }

        /// <summary>
        /// Valida o momento e a situação do jogo.
        /// </summary>
        /// <param name="acao"></param>
        private void ValidaAcaoMomentoJogo(AcaoJogador acao)
        {
            if(Momento.MomentoAtual == TipoRodada.PreJogo)
            {
                if(acao.Acao != TipoAcao.Play)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == TipoRodada.PreFlop)
            {
                if (acao.Acao != TipoAcao.Call)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == TipoRodada.Flop)
            {
                if (acao.Acao != TipoAcao.Check && acao.Acao != TipoAcao.Call && acao.Acao != TipoAcao.Raise)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == TipoRodada.Turn)
            {
                if (acao.Acao != TipoAcao.Check && acao.Acao != TipoAcao.Raise)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == TipoRodada.River)
            {
                if (acao.Acao != TipoAcao.Check)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if (Momento.MomentoAtual == TipoRodada.PosRiver)
            {
                if (acao.Acao != TipoAcao.Check)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == TipoRodada.FimDeJogo)
            {
                if (acao.Acao != TipoAcao.SemAcao)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else
            {
                throw new Exception("Momento de jogo inválido.");
            }
        }

        /// <summary>
        /// Executa uma jogada apenas.
        /// </summary>
        /// <returns></returns>
        public string ExecutaJogada()
        {
            if (Jogador.Momento != Momento.MomentoAtual ||
                Momento.MomentoAtual == TipoRodada.FimDeJogo)
                throw new Exception("Não é possível passar para o próximo momento de jogo, mesa e jogador diferem.");

            // Executa os passos
            ProximoPasso();

            // Salva as informações da rodada
            string SituacaoAtual = GetInfo();

            // Avança o momento
            AvancaMomento();

            return SituacaoAtual;
        }

        /// <summary>
        /// Inicia e executa a rodada que será jogada.
        /// </summary>        
        public string ExecutaRodada()
        {
            string rodada = "";
            this.IniciaRodada();

            while(Momento.MomentoAtual != TipoRodada.FimDeJogo)
                rodada = ExecutaJogada();

            Console.WriteLine(rodada);
            Console.ReadKey();
            return rodada;
        }

        /// <summary>
        /// Dá as cartas ao jogador
        /// </summary>        
        public void DistribuiCartas()
        {
            Jogador.RecebeCarta(Deck.Pop(), Deck.Pop());
            infoMesa.CartasJogador = Jogador.Cartas;
        }

        /// <summary>
        /// Verifica quais jogadores ainda jogarão.
        /// </summary>
        private void PreJogo()
        {
            infoMesa.ReiniciaInfoMesa(UltimoJogadorVencedor);
            IniciaRodada();
        }

        /// <summary>
        /// Verifica se o jogador irá jogar o flop
        /// </summary>
        private void PreFlop()
        {
            DistribuiCartas();
        }

        /// <summary>
        /// Distribui o Flop na mesa.
        /// </summary>
        private void DistribuiFlop()
        {
            Flop = new List<Carta>()
            {
                Deck.Pop(),
                Deck.Pop(),
                Deck.Pop()
            };

            infoMesa.Flop[0] = Flop[0];
            infoMesa.Flop[1] = Flop[1];
            infoMesa.Flop[2] = Flop[2];
        }

        /// <summary>
        /// Distribui o Turn na mesa.
        /// </summary>
        private void DistribuiTurn()
        {
            Turn = Deck.Pop();
            infoMesa.Turn = Turn;
        }

        /// <summary>
        /// Distribui o River na mesa.
        /// </summary>
        private void DistribuiRiver()
        {
            River = Deck.Pop();
            infoMesa.River = River;
        }

        /// <summary>
        /// Dá as cartas à mesa.
        /// </summary>
        private void DistribuiCartasMesa()
        {
            CartasBanca[0] = Deck.Pop();
            CartasBanca[1] = Deck.Pop();
            infoMesa.CartasBanca = CartasBanca;
        }

        /// <summary>
        /// Retorna as casas da mesa Flop, Turn e River;
        /// </summary>
        /// <returns>Mesas da FLop</returns>
        private Carta[] GetMesa() => new Carta[] { Flop[0], Flop[1], Flop[2], River, Turn };

    }
}
