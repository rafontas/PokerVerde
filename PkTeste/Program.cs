using JogadorTH;
using Modelo;
using System.Linq;
using System;
using Comum.Classes;
using JogadorTH.Inteligencia;
using Comum.Interfaces;
using System.IO;
using PkTeste.Classes;
using MaoTH.DAO;
using MaoTH;
using PokerDAO.Base;

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
                .SetRestantePadrao()
                .GetResult();

            SitAndGo.Executa();

            IImprimePartida imp = jogador.ImprimePartida.First();
            string content = imp.pequenoResumo(jogador.Historico) + Environment.NewLine;
            content += "Resumo das Partidas: " + Environment.NewLine;
            content += imp.pequenoResumoTodasPartidas(jogador.Historico);
            content += Environment.NewLine;
            Program.SalvaArquivo(content);
        }

        static void Main(string[] args)
        {
            try
            {
                //CalculadoraProbabilidadeMaosInicial CalcInicial = new CalculadoraProbabilidadeMaosInicial(1000);
                CalculadoraProbabilidadeMaosInicial CalcInicial = new CalculadoraProbabilidadeMaosInicial(500000);
                CalcInicial.Gerar();
                CalcInicial.Salvar();

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
