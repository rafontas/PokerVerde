using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using PokerDAO;
using PokerDAO.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testes.DAO
{
    [TestClass]
    public class AnaliseConvergenciaContextTest
    {

        [TestMethod]
        public void ToInsertQuery()
        {
            int numCarta = 2, qtdJogos = 100;
            float prob = 50.00f;
            TimeSpan tmpExec = new TimeSpan(0, 5, 30);
            DateTime dtExec = DateTime.Now;
            string stat = "";
            Carta[] cartas = new Carta[]
            {
                new Carta(12, Enuns.Naipe.Copas),
                new Carta(13, Enuns.Naipe.Espadas),
            };

            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.analise_convergencia (" +
                    "numero_de_cartas, " + Environment.NewLine +
                    "quantida_de_jogos_executados, " + Environment.NewLine +
                    "probabilidade, " + Environment.NewLine +
                    "tempo_gasto_execucao, " + Environment.NewLine +
                    "dt_inclusaso, " + Environment.NewLine +
                    "status, " + Environment.NewLine +
                    "cartas) " + Environment.NewLine
                );

            strBuilder.AppendFormat("VALUES (" +
                "{0}," + Environment.NewLine +
                "{1}," + Environment.NewLine +
                "{2}," + Environment.NewLine +
                "\'{3}\'," + Environment.NewLine +
                "\'{4}\'," + Environment.NewLine +
                "\'{5}\'," + Environment.NewLine +
                "\'{6}\')",
                numCarta, 
                qtdJogos, 
                prob.ToString("00.00"), 
                DBUteis.ToTimeStampPGSQL(tmpExec), 
                DBUteis.ToDateTimePGSQL(dtExec), 
                stat,
                cartas[0].ToString() + " " + cartas[1].ToString() + " ");

            AnaliseConvergenciaContext analiseContext = new AnaliseConvergenciaContext()
            {
                NumeroDeCartas = numCarta,
                QuantidadeDeJogosExecutados = qtdJogos,
                Probabilidade = prob,
                TempoDeExecução = tmpExec,
                status = stat,
                Cartas = cartas
            };

            string minhaStr = strBuilder.ToString();

            Assert.IsTrue(analiseContext.ToInsertQuery(dtExec) == minhaStr);
        }
    }
}
