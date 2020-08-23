using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDAO
{
    public class AnaliseConvergencia
    {
        public int Id { get; internal set; }

        public int NumeroDeCartas { get; set; }

        public int QuantidadeDeJogosExecutados { get; set; }

        public float Probabilidade { get; set; }

        public TimeSpan TempoDeExecucao { get; set; }

        public string status { get; set; }

        public DateTime DataDeInclusao { get; internal set; }

        public IList<Carta> Cartas { get; set; }

        public void Persiste()
        {
            AnaliseConvergenciaContext analiseContext = new AnaliseConvergenciaContext()
            {
                NumeroDeCartas = this.NumeroDeCartas,
                QuantidadeDeJogosExecutados = this.QuantidadeDeJogosExecutados,
                Probabilidade = this.Probabilidade,
                TempoDeExecução = this.TempoDeExecucao,
                status = this.status,
                Cartas = this.Cartas
            };

            analiseContext.Persiste();
        }

        public static void Persiste(AnaliseConvergencia analiseConvertencia)
        {
            AnaliseConvergenciaContext analiseContext = new AnaliseConvergenciaContext()
            {
                NumeroDeCartas = analiseConvertencia.NumeroDeCartas,
                QuantidadeDeJogosExecutados = analiseConvertencia.QuantidadeDeJogosExecutados,
                Probabilidade = analiseConvertencia.Probabilidade,
                TempoDeExecução = analiseConvertencia.TempoDeExecucao,
                status = analiseConvertencia.status,
                Cartas = analiseConvertencia.Cartas
            };

            analiseConvertencia.Persiste();
        }

        public static void Persiste(IList<AnaliseConvergencia> analises)
        {
            foreach(var a in analises)
                AnaliseConvergencia.Persiste(a);
        }
    }
}
