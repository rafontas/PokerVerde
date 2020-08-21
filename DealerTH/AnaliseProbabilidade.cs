using DealerTH.Probabilidade;
using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MaoTH
{
    public class AnaliseProbabilidade
    {
        const string NOME_ARQUIVO_CONVERGENCIA = "CONVERGENCIA_QUANT_JOGOS.txt";
        public int NumeroCartasAleatorias { get; set; }
        public int ValorMaximo { get; set; }

        private Random random { get; set; } = new Random();

        private IDictionary<string, IList<float>> MaosAleatoriasGeradas { get; set; } = new Dictionary<string, IList<float>>();

        private Carta [] GetNovaMaoIneditaSuited()
            => this.GetNovaMaoInedita(Enuns.Naipe.Espadas, Enuns.Naipe.Espadas);

        private Carta[] GetNovaMaoIneditaOff()
            => this.GetNovaMaoInedita(Enuns.Naipe.Copas, Enuns.Naipe.Espadas);

        private Carta [] GetNovaMaoInedita(Enuns.Naipe carta_1, Enuns.Naipe carta_2)
        {

            int NumUm = this.random.Next(2, 14);
            int NumDois = this.random.Next(2, 14);
            Carta carta1, carta2;
            string novaMao;

            //evita carta de mesmo naipe e número
            while (carta_1 == carta_2 && NumUm == NumDois)
            {
                NumUm = this.random.Next(2, 14);
                NumDois = this.random.Next(2, 14);

            }

            if (NumUm > NumDois)
            {
                novaMao = NumDois.ToString() + NumUm.ToString();
            }
            else
            {
                novaMao = NumUm.ToString() + NumDois.ToString();
            }

            carta1 = new Carta((uint)NumUm, carta_1);
            carta2 = new Carta((uint)NumDois, carta_2);
            novaMao = carta1.ToString() + " " + carta2.ToString();

            while (MaosAleatoriasGeradas.ContainsKey(novaMao))
            {
                NumUm = this.random.Next(2, 14);
                NumDois = this.random.Next(2, 14);

                if (carta_1 == carta_2 && NumUm == NumDois) 
                    continue;

                if (NumUm < NumDois)
                {
                    int aux = NumUm;
                    NumUm = NumDois;
                    NumDois = aux;
                }

                carta1 = new Carta((uint)NumUm, carta_1);
                carta2 = new Carta((uint)NumDois, carta_2);
                novaMao = carta1.ToString() + " " + carta2.ToString();
            }

            return new Carta[] { new Carta((uint)NumUm, carta_1), new Carta((uint)NumDois, carta_2) };
        }
        
        public void AnaliseConvergenciaMaoQuantidadeJogos()
        {
            string fileContent = "Passo: 100.000, Valor Máximo: " + this.ValorMaximo + Environment.NewLine;
            string valoresAnalise = "";
            string valorfinal = "";

            File.AppendAllText(NOME_ARQUIVO_CONVERGENCIA, fileContent);
            Console.WriteLine("Analise de convergência");
            Console.WriteLine("" + fileContent + Environment.NewLine);

            for (int i = 0; i < this.NumeroCartasAleatorias; i++)
            {
                Carta[] maoOff = this.GetNovaMaoIneditaOff();
                string maoAleatoria = maoOff[0].ToString() + " " + maoOff[1].ToString();

                this.MaosAleatoriasGeradas.Add(maoAleatoria, new List<float>());

                fileContent = Environment.NewLine + maoAleatoria + Environment.NewLine;

                Console.WriteLine(fileContent);
                File.AppendAllText(NOME_ARQUIVO_CONVERGENCIA, fileContent);
                valoresAnalise = "";

                for (int k = 1; k < 3; k++)
                {
                    int j = 10000;
                    this.ValorMaximo = 100000;
                    
                    if (k == 2)
                    {
                        this.ValorMaximo = 1000000;
                        j = 100000;
                    }
                    
                    while (j <= this.ValorMaximo)
                    {
                        valoresAnalise += j + "\t";
                        float probabilidadeOff = AvaliaProbabilidadeMao.GetPorcentagemVitoria(maoOff, (uint) j);
                        this.MaosAleatoriasGeradas[maoAleatoria].Add(probabilidadeOff);

                        valorfinal += (maoAleatoria + ";" + j + ";" + probabilidadeOff.ToString() + Environment.NewLine);
                        
                        if (k == 2) j += 100000;
                        else j += 10000;
                    }
                }

                fileContent = "\t" + valoresAnalise + Environment.NewLine + "\t";
                foreach (var item in this.MaosAleatoriasGeradas[maoAleatoria])
                    fileContent += item.ToString("0.0000") + "\t";

                fileContent += Environment.NewLine;
                
                Console.WriteLine(fileContent);
                File.AppendAllText(NOME_ARQUIVO_CONVERGENCIA, fileContent);

                Thread.Sleep(100);
            }

            File.AppendAllText("Grafico.txt", valorfinal);

        }
    }
}
