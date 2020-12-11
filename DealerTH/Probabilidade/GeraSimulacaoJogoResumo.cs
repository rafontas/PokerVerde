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

namespace MaoTH.Probabilidade
{
    public class GeraSimulacaoJogosResumo
    {
        public uint QuantidadeJogosSimuladosPretendidos { get; private set; } = 100000;
        public uint StackInicial { get; private set; } = 10000;
        public IAcaoProbabilidade AcaoProbabilidade { get; set; }

        public IConfiguracaoPoker Config { get; set; } = ConfiguracaoTHBonus.getConfiguracaoPadrao();

        public GeraSimulacaoJogosResumo(uint qtdJogosPorSimulacao)
        {
            this.QuantidadeJogosSimuladosPretendidos = qtdJogosPorSimulacao;
        }
    
        private void SetAcaoProbabilidadePadrao(IProbabilidadeMaoInicial prob)
        {
            this.AcaoProbabilidade = new AcaoProbabilidade()
            {
                probabilidadeMinicaSeeFlop = 50f,
                probabilidadeMinimaRaisePreTurn = 50f,
                probabilidadeMinimaRaisePreRiver = 50f,
            };
        }

        public void SimulaJogosResumoPersiste(int quantidadeSimulacoes, IJogador jogador)
        {
            for (int i = 0; i < quantidadeSimulacoes; i++)
            {
                ISimulacaoJogosResumo s = this.SimulaJogos(jogador);
                this.PersisteSimulacao(s);
            }
        }

        public void PersisteSimulacao(ISimulacaoJogosResumo simulacao) => SimulacaoJogosResumoContext.Persiste(simulacao);

        public ISimulacaoJogosResumo SimulaJogos(IJogador jogador, IAcaoProbabilidade acao = null)
        {
            ISimulacaoJogosResumo simulacao = new SimulacaoJogosResumo()
            {
                QuantidadeJogosSimuladosPretendidos = this.QuantidadeJogosSimuladosPretendidos,
                StackInicial = this.StackInicial,
                QuantidadeJogosSimulados = 0,
                QuantidadeJogosGanhos = 0,
                QuantidadeJogosPerdidos = 0,
                QuantidadeJogosEmpatados = 0,
                DescricaoInteligencia = "Foge se não tiver chance vencer maior que 50% em qualquer decisão tomada",
                StackFinal = 0,
                AcaoProbabilidade = acao
            };

            IJogador banca = new Banca(this.Config);
            Comum.Mesa m = new Comum.Mesa(this.Config);

            ICroupier croupier = new Croupier(new CroupierConstructParam()
            {
                Jogador = jogador,
                Banca = banca,
                ConfiguracaoPoker = this.Config
            }
            );

            simulacao = this.GeraUmaSimulacao(simulacao, croupier, jogador);

            return simulacao;
        }

        private ISimulacaoJogosResumo GeraUmaSimulacao(ISimulacaoJogosResumo simulacao, ICroupier croupier, IJogador jogador)
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
