using Comum;
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
using System.Threading;

namespace MaoTH.Probabilidade
{
    public class GeraSimulacaoJogosResumo
    {
        public uint QuantidadeJogosSimuladosPretendidos { get; private set; } = 100000;
        public uint StackInicial { get; private set; } = 10000;
        public IAcaoProbabilidade AcaoProbabilidade { get; set; }

        public static string ResumoMaoJogador { get; set; }
        public bool ModoVerboso { get; set; } = false;

        private string UltimaMensagemImpressa { get; set; } = string.Empty;

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

        private void PrintaProgressoConsole(string entrada)
        {
            if ((Console.CursorLeft - this.UltimaMensagemImpressa.Length) > 0)
                Console.CursorLeft = (Console.CursorLeft - this.UltimaMensagemImpressa.Length);

            Console.Write(entrada);
            this.UltimaMensagemImpressa = entrada;
        }

        private ISimulacaoJogosResumo GeraUmaSimulacao(ISimulacaoJogosResumo simulacao, ICroupier croupier, IJogador jogador)
        {
            string impressaoModoVerboso = string.Empty;
            int quantTestesSeguidos = 0;

            for (int i = 0; i < this.QuantidadeJogosSimuladosPretendidos; i++)
            {
                Uteis.ImprimeAgora = string.Empty;

                if (!croupier.HaParticipantesParaJogar()) break;

                if (this.ModoVerboso && (i % 50) == 0)
                {
                    if (quantTestesSeguidos-- <= 0)
                    {
                        Uteis.ModoVerboso = false;
                    }
                    else
                    {
                        //this.PrintaProgressoConsole(string.Format("{0}/{1}", i, this.QuantidadeJogosSimuladosPretendidos));
                        Uteis.ModoVerboso = true;
                        quantTestesSeguidos = 2;
                    }
                }

                croupier.ExecutarNovaPartidaCompleta();
                simulacao.QuantidadeJogosSimulados++;
                IPartida p = jogador.Historico.Last();

                if (Uteis.ModoVerboso)
                {
                    Console.WriteLine(Uteis.ImprimeAgora + " " + p.Jogador.Stack.ToString("0,00"));
                }

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
