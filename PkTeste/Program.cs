using JogadorTH;
using Mesa;
using Modelo;
using System.Linq;
using System;
using System.Collections.Generic;
using DealerTH;
using PkTeste.Interfaces;

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

        static void Main(string[] args)
        {
            MesaTexasHoldem mesa = new MesaTexasHoldem(
                Program.configPadrao, 
                new JogadorBase(),
                new Dealer(),
                10
            );


            IPokerComandos cmBasicos = new ComandosBasicos();
            Console.WriteLine(cmBasicos.getHelp());
            
            bool saiPrograma = false;

            while (!saiPrograma)
            {
                string entradas = Console.ReadLine(), input = "";
                string [] inputCompleta = entradas.Split(" ");
                int numRodadas = 1;

                if (input.Count() > 2)
                {
                    Console.WriteLine("Número de argumentos inválidos");
                    continue;
                }
                else if (inputCompleta.Count() > 1)
                {
                    input = inputCompleta[0];
                    numRodadas = int.Parse(inputCompleta[1]);
                }
                else
                {
                    input = inputCompleta[0];
                }

                switch (input)
                {
                    case "-e": 
                        saiPrograma = true; break;
                    case "-tr":
                        input = Console.ReadLine();
                        ExecutaPoker(mesa, numRodadas);
                        break;
                    case "-t":
                        Console.WriteLine("Modo teste, aperte -p para ir para próximo passo.");
                        ExecutaPokerPausado(mesa);
                        break;
                    case "-cls": 
                        Console.Clear(); 
                        Console.WriteLine("Esperando comando..."); break;
                    case "s": break;
                    case "-i":
                        ExecutaPoker(mesa, 1);
                        break;
                    default:
                        Console.WriteLine("Não entendi.");
                        Console.WriteLine(cmBasicos.getHelp());
                        break;
                }
            }
        }

        public static void ExecutaPokerPausado(MesaTexasHoldem mesa)
        {
            IList<string> ultimaJogada = new List<string>();
            IPokerComandos comandosAnalise = new ComandosNaAnalise();

            Console.Clear();
            Console.WriteLine("Bem vindo ao poker pausado, as opções são:" + comandosAnalise.getHelp());
            
            bool saiPrograma = false;

            while (!saiPrograma)
            {
                string input = "";
                switch (input)
                {
                    case "i":
                        Console.WriteLine(comandosAnalise.getHelp());
                        break;
                    case "e":
                        saiPrograma = true;
                        break;
                    case "r":
                        Console.WriteLine(ultimaJogada.Last());
                        break;
                    case "c": 
                        Console.Clear(); 
                        Console.WriteLine("Esperando comando..."); 
                        break;
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
                            Console.WriteLine(comandosAnalise.getHelp());
                        }
                        break;
                    default:
                        ultimaJogada.Add(mesa.ExecutaJogada().ToString());
                        Console.WriteLine(Environment.NewLine + ultimaJogada.Last());
                        saiPrograma = (mesa.Momento.MomentoAtual == Enuns.TipoRodada.FimDeJogo);
                        break;
                }
            }
        }

        public static void ExecutaPoker(MesaTexasHoldem mesa, int numeroRodadas)
        {
            for (int i = 0; i < numeroRodadas; i++) 
                mesa.ExecutaRodada();
        }
    }
}
