using Enuns;
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

        public Deck Deck { get; set; }

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
            Deck = new Deck();
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

            if (Momento.MomentoAtual == MomentoJogo.PreJogo)
            {
                PreJogo();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, ConfigMesa.Ant);
            }
            else if (Momento.MomentoAtual == MomentoJogo.PreFlop)
            {
                PreFlop();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, ConfigMesa.Flop);
            }
            else if (Momento.MomentoAtual == MomentoJogo.Flop)
            {
                DistribuiFlop();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, ConfigMesa.Turn);
            }
            else if (Momento.MomentoAtual == MomentoJogo.Turn)
            {
                DistribuiTurn();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, ConfigMesa.River);
            }
            else if (Momento.MomentoAtual == MomentoJogo.River)
            {
                DistribuiRiver();
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, 0);
                DistribuiCartasMesa();
            }
            else if (Momento.MomentoAtual == MomentoJogo.PosRiver)
            {
                acao = Jogador.ExecutaAcao(Momento.MomentoAtual, 0);
            }
            else if (Momento.MomentoAtual == MomentoJogo.FimDeJogo)
                return;
            else
                throw new Exception("O tipo de jogo requisitado não existe.");

            ManipulaAcaoJogador(acao);
        }

        /// <summary>
        /// Avança o momento de jogo da mesa quanto do jogador
        /// </summary>
        /// <returns></returns>
        public MomentoJogo AvancaMomento()
        {
            infoMesa.Momento = Momento.Proximo();
            Jogador.AvancaMomento();
            return infoMesa.Momento;
        }

        /// <summary>
        /// Manipula a Ação de um jogador.
        /// </summary>
        /// <param name="acao">A ação tomada pelo jogador.</param>
        /// <returns>O momento data a ação do jogador.</returns>
        private void ManipulaAcaoJogador(AcaoJogador acao)
        {
            ValidaAcaoMomentoJogo(acao);

            // Identifica o fim de jogo
            if (acao.Acao == TipoAcao.Fold)
            {
                Momento.TerminaJogo();
                infoMesa.Momento = Momento.MomentoAtual;
                infoMesa.GanhosMesa += infoMesa.GetPote();
                infoMesa.ReiniciaInfoMesa(TipoJogadorTHB.Mesa);
            }
            else if (acao.Acao == TipoAcao.Check)
            {
                if (Momento.MomentoAtual == MomentoJogo.PosRiver)
                {
                    if (Dealer.JogadorGanhouTHB(GetMesa(), Jogador.GetCartas, CartasBanca))
                    {
                        Jogador.RecebeValor(infoMesa.GetPote());
                        infoMesa.ReiniciaInfoMesa(TipoJogadorTHB.Jogador);
                    }
                    else
                    {
                        infoMesa.GanhosMesa += infoMesa.GetPote();
                        infoMesa.ReiniciaInfoMesa(TipoJogadorTHB.Mesa);
                    }

                    Momento.TerminaJogo();
                    infoMesa.Momento = Momento.MomentoAtual;
                }
            }
            else if (acao.Acao == TipoAcao.Call)
            {
                RequisitaPagamentoJogador(acao);
            }
            else if (acao.Acao == TipoAcao.Raise)
            {
                RequisitaPagamentoJogador(acao);
            }
            else if (acao.Acao == TipoAcao.Play)
            {
                RequisitaPagamentoJogador(acao);
            }
            else if (acao.Acao == TipoAcao.Stop)
            {
                Momento.TerminaJogo();
            }
        }

        /// <summary>
        /// Requisita o pagamento de um jogador de acordo com o momento do jogo.
        /// </summary>
        /// <param name="acao">A ação do jogador</param>
        public void RequisitaPagamentoJogador(AcaoJogador acao)
        {
            if (Momento.MomentoAtual == MomentoJogo.PreJogo)
            {
                Jogador.PagaValor(ConfigMesa.Ant);
                infoMesa.ValorInvestidoAnt = ConfigMesa.Ant;
            }
            else if (Momento.MomentoAtual == MomentoJogo.PreFlop)
            {
                Jogador.PagaValor(ConfigMesa.Flop);
                infoMesa.ValorInvestidoFlop = ConfigMesa.Flop;
            }
            else if (Momento.MomentoAtual == MomentoJogo.Flop)
            {
                Jogador.PagaValor(ConfigMesa.Turn);
                infoMesa.ValorInvestidoTurn = ConfigMesa.Turn;
            }
            else if (Momento.MomentoAtual == MomentoJogo.Turn)
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
            if(Momento.MomentoAtual == MomentoJogo.PreJogo)
            {
                if(acao.Acao != TipoAcao.Play)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == MomentoJogo.PreFlop)
            {
                if (acao.Acao != TipoAcao.Call)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == MomentoJogo.Flop)
            {
                if (acao.Acao != TipoAcao.Check && acao.Acao != TipoAcao.Call && acao.Acao != TipoAcao.Raise)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == MomentoJogo.Turn)
            {
                if (acao.Acao != TipoAcao.Check && acao.Acao != TipoAcao.Raise)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == MomentoJogo.River)
            {
                if (acao.Acao != TipoAcao.Check)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if (Momento.MomentoAtual == MomentoJogo.PosRiver)
            {
                if (acao.Acao != TipoAcao.Check)
                    throw new Exception("Ação ilegal para momento de jogo.");
            }
            else if(Momento.MomentoAtual == MomentoJogo.FimDeJogo)
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
            if (Jogador.Momento != Momento.MomentoAtual)
                throw new Exception("Não é possível passar para o próximo momento de jogo, mesa e jogador diferem.");

            ProximoPasso();

            string SituacaoAtual = GetInfo();

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

            while(Momento.MomentoAtual != MomentoJogo.FimDeJogo)
            {
                rodada = ExecutaJogada();
            }

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
            infoMesa.CartasJogador = Jogador.GetCartas;
        }

        /// <summary>
        /// Verifica quais jogadores ainda jogarão.
        /// </summary>
        private void PreJogo()
        {
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
