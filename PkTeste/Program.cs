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

        static void GeraSimulacoesVariaves()
        {
            IAcaoProbabilidade acaoProbabilidade;

            float[] rangeValoresMinimosCallFlop = new float[] { 37f, 40f, 42f, 44f, 46f, 48f, 50f };
            float[] rangeValoresMinimosRaisePreTurn = new float[] { 37f, 40f, 42f, 44f, 46f, 48f, 50f };
            float[] rangeValoresMinimosRaisePreRiver = new float[] { 37f, 40f, 42f, 44f, 46f, 48f, 50f };

            foreach(float minCallFop in rangeValoresMinimosCallFlop)
            {
                Console.WriteLine("Item: " + minCallFop);

                foreach(float minRaisePreTurn in rangeValoresMinimosCallFlop)
                {
                    Console.WriteLine("Item: " + minCallFop + " " + minRaisePreTurn);

                    foreach(float minRaisePreRiver in rangeValoresMinimosCallFlop)
                    {
                        acaoProbabilidade = new AcaoProbabilidade()
                        {
                            probabilidadeMinicaSeeFlop = minCallFop,
                            probabilidadeMinimaRaisePreTurn = minRaisePreTurn,
                            probabilidadeMinimaRaisePreRiver = minRaisePreRiver
                        };


                        if (AcaoProbabilidadeContexto.ExisteItem(acaoProbabilidade)) continue;

                        Console.WriteLine("Item: " + minCallFop + " " + minRaisePreTurn + " " + minRaisePreRiver);

                        Program.SimulaJogadorProbabilistico(acaoProbabilidade);

                        // Fazer aqui para gerar as paradas.
                    }

                }
            }
        }

        static void SimulaJogadorProbabilistico(IAcaoProbabilidade acaoProbabilidade)
        {
            
            GeraSimulacaoJogosResumo geraSimulacao = new GeraSimulacaoJogosResumo(1200);
            
            for (int i = 0; i < 2; i++)
            {

                IJogador jogador = new JogadorProbabilistico(
                    Program.configPadrao,
                    acaoProbabilidade,
                    new RecuperaProbabilidade(),
                    10000
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
