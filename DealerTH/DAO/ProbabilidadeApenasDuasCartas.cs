using DealerTH.Probabilidade;
using Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MaoTH.DAO
{
    public class ProbabilidadeApenasDuasCartas
    {
        private const string NOME_ARQUIVO_PROB = "PROB_DUAS_CARTAS.csv";
        private const uint QUANT_JOGOS_POR_PROB = 500000;
        public IDictionary<string, float> ProbabilidadesOff { get; set; }
        
        public IDictionary<string, float> ProbabilidadesSuited { get; set; }

        private bool HaProbabilidadeSalva() => File.Exists(NOME_ARQUIVO_PROB);
        
        public void Carregar() { }

        public void Gerar() 
        {
            uint quantidadeJogos = QUANT_JOGOS_POR_PROB;
            this.ProbabilidadesOff = new Dictionary<string, float>();

            for (uint i = 2; i <= 14; i++)
            {
                for (uint j = i; j <= 14; j++)
                {
                    string chaveMaoOff = i.ToString() + " " + j.ToString();

                    Carta [] maoOff = new Carta[] {
                        new Carta(i, Enuns.Naipe.Copas),
                        new Carta(j, Enuns.Naipe.Espadas)
                    };

                    float probabilidadeOff = AvaliaProbabilidadeMao.GetPorcentagemVitoria(maoOff, quantidadeJogos);

                    ProbabilidadesOff.Add(chaveMaoOff, probabilidadeOff);
                }
            }    
        }
        
        private void Salvar() 
        {
            StringBuilder str = new StringBuilder();

            // Salva os itens Off
            if (this.ProbabilidadesOff != null)
            {
                foreach(KeyValuePair<string, float> item in this.ProbabilidadesOff)
                    str.AppendFormat("{0}:{1}; ", item.Key, item.Value);

                str.AppendLine();
            }

            if(this.ProbabilidadesSuited != null)
            {
                // Salva os itens suited
                foreach (KeyValuePair<string, float> item in this.ProbabilidadesSuited)
                    str.AppendFormat("{0}:{1};", item.Key, item.Value);
            }

            File.WriteAllText(NOME_ARQUIVO_PROB, str.ToString());
        }
        
        public ProbabilidadeApenasDuasCartas() 
        {
            if (this.HaProbabilidadeSalva())
            {
                this.Carregar();
            }
            else
            {
                this.Gerar();
                this.Salvar();
            }
        }
    }
}
