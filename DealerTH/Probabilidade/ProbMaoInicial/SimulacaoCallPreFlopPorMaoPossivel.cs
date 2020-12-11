using Comum.Classes;
using Comum.Classes.Poker;
using Comum.Classes.Poker.AnaliseProbabilidade;
using Comum.Interfaces;
using Comum.Interfaces.AnaliseProbabilidade;
using JogadorTH;
using Modelo;
using PokerDAO.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaoTH.Probabilidade.ProbMaoInicial
{
    public class SimulacaoCallPreFlopPorMaoPossivel
    {
        public uint QuantidadeJogosSimuladosPretendidos { get; private set; } = 100000;
        public uint StackInicial { get; private set; } = 10000;

        public IConfiguracaoPoker Config { get; set; } = ConfiguracaoTHBonus.getConfiguracaoPadrao();

        public SimulacaoCallPreFlopPorMaoPossivel(uint qtdJogosSimulacao)
        {
            this.QuantidadeJogosSimuladosPretendidos = qtdJogosSimulacao;
        }
    
        private bool ExisteSimulacao(Carta c1, Carta c2, uint qtdJogosSimulacao = 500000)
        {
            return this.ExisteSimulacao(MaoBasica.ToMao(c1, c2), qtdJogosSimulacao);
        }

        private bool ExisteSimulacao(IMaoBasica maoBasica, uint qtdJogosSimulacao = 500000)
        {
            return SimulacaoCallPreFlopProbabilidadeContext.JaExisteProbabilidadeCadastrada(maoBasica);
        }

        private Carta [] GetCartas(IMaoBasica mao)
        {
            Carta[] cartas = new Carta[2];

            if (mao.OffOrSuited == 'O')
            {
                cartas[1] = new Carta(mao.NumCarta1, Enuns.Naipe.Copas);
                cartas[0] = new Carta(mao.NumCarta2, Enuns.Naipe.Espadas);
            }
            else if (mao.OffOrSuited == 'S')
            {
                cartas[1] = new Carta(mao.NumCarta1, Enuns.Naipe.Copas);
                cartas[0] = new Carta(mao.NumCarta2, Enuns.Naipe.Copas);
            }
            else {
                throw new Exception("Tipo de carta não encontrada");
            }

            return cartas;
        }

        private IAcaoProbabilidade GetAcaoProbabilidade(IProbabilidadeMaoInicial prob)
        {
            return new AcaoProbabilidade()
            {
                probabilidadeMinicaSeeFlop = prob.ProbabilidadeVitoria,
                probabilidadeMinimaRaisePreTurn = 50f,
                probabilidadeMinimaRaisePreRiver = 50f,
            };
        }

        public void GeraListaCallPreFlopLimiteProbabilidade()
        {
            int idGrupo = 5;
            this.QuantidadeJogosSimuladosPretendidos = 1000;

            for (int i = 0; i < 10; i++)
            {
                this.GeraListaGanhosPerdas(idGrupo++);
            }
        }

        public IList<ISimulacaoCallPreFlop> GeraListaGanhosPerdas(int idGrupo)
        {
            IList<IMaoBasica> listaMao = MaoBasica.GetTodasMaosPossiveis();
            IList<ISimulacaoCallPreFlop> listaSimulacao = new List<ISimulacaoCallPreFlop>();
            IJogador banca = new Banca(this.Config);
            IRetornaProbabilidade retornaProbabilidade = new RecuperaProbabilidade();
            Comum.Mesa m = new Comum.Mesa(this.Config);
            IList<IMaoBasica> novaMao = new List<IMaoBasica>();

            //foreach (IMaoBasica mao in listaMao)
            //    if (mao.NumCarta1 > 10) novaMao.Add(mao);
            //listaMao = novaMao;


            foreach (IMaoBasica mao in listaMao)
            {
                ISimulacaoCallPreFlop simulacao = new SimulacaoCallPreFlop()
                {
                    IdGrupo = idGrupo,
                    ProbabilidadeMaoInicial = ProbabilidadeMaoInicialContext.GetItem(mao),
                    MaoBasica = mao,
                    QuantidadeJogosSimuladosPretendidos = this.QuantidadeJogosSimuladosPretendidos,
                    StackInicial = this.StackInicial,
                    QuantidadeJogosSimulados = 0,
                    QuantidadeJogosGanhos = 0,
                    QuantidadeJogosPerdidos = 0,
                    QuantidadeJogosEmpatados = 0,
                    RaiseFlop = false,
                    RaiseFlopTurn = false
                };

                IJogador jogador = new JogadorProbabilistico(
                    this.Config, 
                    this.GetAcaoProbabilidade(simulacao.ProbabilidadeMaoInicial),
                    retornaProbabilidade, 
                    this.StackInicial
                );

                ICroupier croupier = new Croupier(new CroupierConstructParam() {
                        Jogador = jogador,
                        Banca = banca,
                        ConfiguracaoPoker = this.Config
                    }
                );

                simulacao = this.SimulaJogosUmaMao(simulacao, croupier, jogador);

                SimulacaoCallPreFlopProbabilidadeContext.Persiste(simulacao);

                Console.WriteLine(mao.NumCarta1 + " " + mao.NumCarta2 + " - " + mao.OffOrSuited);
            }

            return listaSimulacao;
        }

        private ISimulacaoCallPreFlop SimulaJogosUmaMao(ISimulacaoCallPreFlop simulacao, ICroupier croupier, IJogador jogador)
        {
            for (int i = 0; i < this.QuantidadeJogosSimuladosPretendidos; i++)
            {
                if (!croupier.HaParticipantesParaJogar()) break;

                croupier.ExecutarNovaPartidaCompleta();
                simulacao.QuantidadeJogosSimulados++;
                IPartida p = jogador.Historico.Last();

                if (p.JogadorGanhador == Enuns.VencedorPartida.Jogador)
                {
                    simulacao.QuantidadeJogosGanhos++;
                }
                else if (p.JogadorGanhador == Enuns.VencedorPartida.Banca)
                {
                    simulacao.QuantidadeJogosPerdidos++;
                }
                else
                {
                    simulacao.QuantidadeJogosEmpatados++;
                }
            }
            
            simulacao.StackFinal = jogador.JogadorStack.Stack;

            return simulacao;
        }
    }
}
