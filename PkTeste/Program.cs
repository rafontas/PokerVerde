using System.Linq;
using System;
using System.IO;
using JogadorTH;
using Modelo;
using Comum.Classes;
using Comum.Interfaces;
using Comum.Classes.Poker;
using JogadorTH.Inteligencia;
using PokerDAO.Base;
using PokerDAO.Contextos;
using MaoTH.Probabilidade.ProbMaoInicial;
using Comum.HoldemHand;
using Comum.Interfaces.AnaliseProbabilidade;
using Comum.Classes.Poker.AnaliseProbabilidade;
using MaoTH;
using MaoTH.Probabilidade;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace PkTeste
{
    public class Program
    {
        static ConfiguracaoTHBonus configPadrao
        {
            get
            {
                return new ConfiguracaoTHBonus()
                {
                    Ant = 5,
                    Flop = 10,
                    Turn = 5,
                    River = 5,
                };
            }
        }

        static void SalvaArquivo(string conteudoArquivo)
        {
            string nome = "resumo_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt";
            File.WriteAllText("D:\\Rafael\\Poker\\" + nome, conteudoArquivo);
        }

        static void ExecutaCorrida()
        {
            IJogador jogador = new DummyJogadorTHB(Program.configPadrao, 1000, new DummyInteligencia());
            jogador.Corrida = new Corrida(2);

            ISitAndGo SitAndGo =
                new BuilderSitAndGo(new ConfiguracaoTHBonus())
                .addJogador(jogador)
                .ToSitAndGo();

            SitAndGo.Executa();

            IImprimePartida imp = jogador.ImprimePartida.First();
            string content = imp.pequenoResumo(jogador.Historico) + Environment.NewLine;
            content += "Resumo das Partidas: " + Environment.NewLine;
            content += imp.pequenoResumoTodasPartidas(jogador.Historico);
            content += Environment.NewLine;
            Program.SalvaArquivo(content);
        }

        private static int UltimaContagemMaosProbCache { get; set; } = 0;
        private static TimeSpan UltimaExecTempo { get; set; }

        static void SalvaSimulacao(string dadosSimulacao)
        {
            string nomeArquivo = "GeraSimulacao.txt", stringDiferencaPersistidos = string.Empty;
            int itensPersistidos = MaoProbabilidadeContexto.GetQuantidadeItensPersistidos();
            int diferencaPersistidos = (Program.UltimaContagemMaosProbCache == 0 ? 0 : itensPersistidos - Program.UltimaContagemMaosProbCache);

            stringDiferencaPersistidos = (diferencaPersistidos == 0) ? " _.___" : ("+" + diferencaPersistidos.ToString("0,00"));

            StringBuilder stringBuilder = new StringBuilder(dadosSimulacao);

            stringBuilder.AppendFormat(" - Itens Persistidos: {0} ({1}){2}", 
                itensPersistidos.ToString("0,00"),
                stringDiferencaPersistidos,
                RecuperarProbabilidade.ResumoCache
            );

            Program.UltimaContagemMaosProbCache = itensPersistidos;
            File.AppendAllText(nomeArquivo, stringBuilder.ToString());
            //Console.WriteLine(stringBuilder.ToString());
            //Console.Beep();
        }

        static void GeraSimulacoesVariaves()
        {
            IAcaoProbabilidade acaoProbabilidade;
            string diferencaTempo = string.Empty;

            Console.WriteLine("Iniciou...");

            float[] rangeValoresMinimosCallFlop = new float[] { 37f, 40f, 42f, 44f, 46f, 48f, 50f };
            float[] rangeValoresMinimosRaisePreTurn = new float[] { 37f, 40f, 42f, 44f, 46f, 48f, 50f };
            float[] rangeValoresMinimosRaisePreRiver = new float[] { 37f, 40f, 42f, 44f, 46f, 48f, 50f };
            Stopwatch sw = new Stopwatch();

            uint qtdJogosPorSimulacao = 1200, qtdSimulacoesPorProbabilidade = 2, stackInicial = 10000;
            int progresso = 0, numeroDeIteracoes = rangeValoresMinimosCallFlop.Count() * rangeValoresMinimosRaisePreTurn.Count() * rangeValoresMinimosRaisePreRiver.Count();

            StringBuilder strBuilder = new StringBuilder();

            foreach (float minCallFop in rangeValoresMinimosCallFlop)
            {
                foreach(float minRaisePreTurn in rangeValoresMinimosCallFlop)
                {
                    foreach(float minRaisePreRiver in rangeValoresMinimosCallFlop)
                    {
                        acaoProbabilidade = new AcaoProbabilidade()
                        {
                            probabilidadeMinicaSeeFlop = minCallFop,
                            probabilidadeMinimaRaisePreTurn = minRaisePreTurn,
                            probabilidadeMinimaRaisePreRiver = minRaisePreRiver
                        };

                        progresso++;
                        if (AcaoProbabilidadeContexto.ExisteItem(acaoProbabilidade)) continue;

                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat(Environment.NewLine + "Prog.: {0}/{1} - Fazendo: {2}, {3}, {4} - Run/QtdJogos {5}/{6} ",
                            progresso,
                            numeroDeIteracoes,
                            minCallFop,
                            minRaisePreTurn,
                            minRaisePreRiver,
                            qtdSimulacoesPorProbabilidade,
                            qtdJogosPorSimulacao
                        );
                        
                        Console.Write(strBuilder.ToString());
                        
                        sw.Reset();
                        sw.Start();
                        Program.SimulaJogadorProbabilistico(acaoProbabilidade, qtdJogosPorSimulacao, qtdSimulacoesPorProbabilidade, stackInicial);
                        sw.Stop();
                        TimeSpan ts = sw.Elapsed;

                        strBuilder.AppendFormat("- Tempo: {0:D2}:{1:D2}", 
                            ts.Minutes, ts.Seconds
                        );
                        
                        if (diferencaTempo != string.Empty)
                        {
                            diferencaTempo = ", ";
                            TimeSpan diff = Program.UltimaExecTempo.Subtract(ts);

                            if (Program.UltimaExecTempo > ts)
                            {
                                diferencaTempo += "-";
                            }
                            else if (Program.UltimaExecTempo < ts)
                            {
                                diff = diff.Negate();
                                diferencaTempo += "+";
                            }
                            else
                            {
                                diferencaTempo += " ";
                            }

                            diferencaTempo += string.Format("({0:D2}:{1:D2})", diff.Minutes, diff.Seconds);
                        }
                        else
                        {
                            diferencaTempo = ",  (__:__)";
                        }

                        strBuilder.Append(diferencaTempo);

                        Program.UltimaExecTempo = sw.Elapsed;
                        Console.Write(strBuilder.ToString());
                        SalvaSimulacao(strBuilder.ToString());
                        MaoProbabilidadeContexto.PersisteItensRestantes();
                    }
                }
            }
        }

        static void SimulaJogadorProbabilistico(IAcaoProbabilidade acaoProbabilidade, uint qtdJogosPorSimulacao, uint qtdSimulacoesPorProbabilidade, uint stackInicial)
        {
            
            GeraSimulacaoJogosResumo geraSimulacao = new GeraSimulacaoJogosResumo(qtdJogosPorSimulacao);
            
            for (int i = 0; i < qtdSimulacoesPorProbabilidade; i++)
            {

                IJogador jogador = new JogadorProbabilistico(
                    Program.configPadrao,
                    acaoProbabilidade,
                    new RecuperarProbabilidade(),
                    stackInicial
                );

                ISimulacaoJogosResumo s = geraSimulacao.SimulaJogos(jogador, acaoProbabilidade);
                geraSimulacao.PersisteSimulacao(s); 
            }
        }

        static void TestHandOdds()
        {
            string board = "2d kh qh 3h qc";
            Hand h1 = new Hand("ad jd", board);
            Hand h2 = new Hand("2h 3d", board);

            if (h1 > h2)
            {
                Console.WriteLine("Yeap");
            }
            else if (h1 < h2)
            {
                Console.WriteLine("Nope");
            }
            else
            {
                Console.WriteLine("Draw");
            }
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            try
            {
                Program.GeraSimulacoesVariaves();

                Console.Beep();
                Thread.Sleep(1000);
                Console.Beep();
                Thread.Sleep(1000);
                Console.Beep();
                //Program.PreencheCallPreFlop();
                //Program.PreencheCallPreFlop();

                //ProbabilidadeApenasDuasCartasContext.AtualizaPorNumerosOffOuSuitedQtdJogosSimulados(list);

                //CalculadoraProbabilidadeMaosInicial CalcInicial = new CalculadoraProbabilidadeMaosInicial(1000);
                //CalculadoraProbabilidadeMaosInicial CalcInicial = new CalculadoraProbabilidadeMaosInicial(500000);
                //CalcInicial.Gerar();
                //CalcInicial.Salvar();

                //AnaliseConvergenciaProbabilidadePorJogosSimulados analiseProbabilidade = new AnaliseConvergenciaProbabilidadePorJogosSimulados()
                //{
                //    NumeroCartasAleatorias = 5,
                //    LimiteMaximoJogosSimulados = 1400000,
                //    QuantidadeInicialJogosSimulados = 100000,
                //    PassoSimulacoes = 200000
                //};

                //analiseProbabilidade.AnaliseConvergenciaMaoQuantidadeJogos();
            }
            finally
            {
                if (DBConnect.EstouConectado()) DBConnect.FecharConexao();
            }
        }

        static void TestaBanco()
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            if (DBConnect.EstouConectado())
            {
                Console.WriteLine("Estou funcionando!");
            }
            else
            {
                Console.WriteLine("Não estou funcionando");
            }
            DBConnect.FecharConexao();
        }
    }
}
