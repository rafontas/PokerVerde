using JogadorTH;
using Mesa;
using Modelo;
using System.Linq;
using System;
using System.Collections.Generic;

namespace PkTeste
{
    public class Program
    {
        static string help = @"Bem vindo ao Poker Verde. As opções são:
                -e : para sair do programa;
                -i : iniciar o jogo texas hold'em bônus;
                -t : iniciar o jogo teste, pausadamente;
                -cls : limpa o console;
                -s : para mostrar o status do jogo no momento;";

        static string pokerPausado = @"
                -cls : limpa o console;
                -e : cancelar e sair do programa;
                -p : próximo passo;
                -r : repetir a rodada anterior;
                -m [Número rodada]: repete uma rodada específica";

        static void Main(string[] args)
        {
            Console.WriteLine(Program.help);
            MesaTexasHoldem mesa = new MesaTexasHoldem(
                new ConfiguracaoTHBonus()
            {
                Ant = 5,
                Flop = 10,
                Turn = 5,
                River = 5,
            }, 
                new Jogador(),
                null,
                10
            );

            ExecutaPoker(mesa, 5);
            return;

            while (true)
            {
                string input = Console.ReadLine();
                bool saiPrograma = false;

                switch (input)
                {
                    case "-e": saiPrograma = true; break;
                    //case "-p":
                    //    Program.ExecutaPokerPausado(mesa);
                    //    break;
                    case "-t":
                        Console.WriteLine("Modo teste, aperte -p para ir para próximo passo.");
                        Program.ExecutaPokerPausado(mesa);
                        break;
                    case "-cls": Console.Clear(); Console.WriteLine("Esperando comando..."); break;
                    case "s": break;
                    case "-i":
                        ExecutaPoker(mesa, 1);
                        break;
                    default:
                        Console.WriteLine("Não entendi.");
                        Console.WriteLine(help);
                        break;
                }

                if (saiPrograma) break;
            }
        }

        public static void ExecutaPokerPausado(MesaTexasHoldem mesa)
        {
            IList<string> ultimaJogada = new List<string>();
            Console.Clear();
            Console.WriteLine("Bem vindo ao poker pausado, as opções são:" + pokerPausado);

            while (true)
            {
                bool saiPrograma = false;
                //string input = Console.Read().ToString();
                string input = "";
                switch (input)
                {
                    case "i":
                        Console.WriteLine(pokerPausado);
                        break;
                    case "e":
                        saiPrograma = true;
                        break;
                    case "r":
                        Console.WriteLine(ultimaJogada.Last());
                        break;
                    case "c": Console.Clear(); Console.WriteLine("Esperando comando..."); break;
                    case "m":
                        Console.WriteLine("Digite o número da rodada: 1 - " + ultimaJogada.Count + 1);
                        input = Console.ReadLine();
                        int numeroRodada = 0;
                        if (int.TryParse(input, out numeroRodada))
                        {
                            Console.WriteLine(ultimaJogada[numeroRodada]);
                        }
                        else
                        {
                            Console.WriteLine("Não entendi. As opções são:");
                            Console.WriteLine(pokerPausado);
                        }
                        break;
                    default:
                        string saida = mesa.ExecutaJogada().ToString();
                        Console.WriteLine(Environment.NewLine + saida);
                        ultimaJogada.Add(saida);
                        break;
                }

                if (saiPrograma) break;
            }
        }

        public static void ExecutaPoker(MesaTexasHoldem mesa, int numeroRodadas)
        {
            for (int i = 0; i < numeroRodadas; i++)
                mesa.ExecutaRodada();
        }
    }
}
