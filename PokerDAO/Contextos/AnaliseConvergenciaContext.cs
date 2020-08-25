using Microsoft.EntityFrameworkCore;
using Modelo;
using PokerDAO.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace PokerDAO
{
    [Table("analise_convergencia")]
    public class AnaliseConvergenciaContext : DbContext
    { 
        [Column("id")]
        public int Id { get; set; }
        
        [Column("numero_de_cartas")]
        public int NumeroDeCartas { get; set; }
        
        [Column("quantidade_de_jogos_executados")]
        public int QuantidadeDeJogosExecutados { get; set; }
        
        [Column("probabilidade")]
        public float Probabilidade { get; set; }

        [Column("tempo_gasto_execucao")]
        public TimeSpan TempoDeExecução { get; set; }

        [Column("status")]
        public string status { get; set; }

        [Column("dt_inclusao")]
        public DateTime DataDeInclusao { get; set; }

        [Column("cartas")]
        public IList<Carta> Cartas { get; set; }

        private bool ValidarInclusao()
        {
            if (this.NumeroDeCartas <= 0) return false;
            if (this.QuantidadeDeJogosExecutados <= 0) return false;
            if (this.Probabilidade < 0) return false;
            if (this.Cartas.Count <= 0) return false;

            return true;
        }

        public string ToInsertQuery(DateTime ? dtInclusao = null) 
        {
            if (!this.ValidarInclusao()) return string.Empty;

            this.DataDeInclusao = dtInclusao ?? DateTime.Now;
            string cartas = "";

            foreach(Carta c in this.Cartas)
                cartas += c.ToString() + " ";

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
                this.NumeroDeCartas,
                this.QuantidadeDeJogosExecutados,
                this.Probabilidade.ToString("00.00"),
                DBUteis.ToTimeStampPGSQL(this.TempoDeExecução),
                DBUteis.ToDateTimePGSQL(this.DataDeInclusao),
                this.status,
                cartas);

            return strBuilder.ToString();

        }

        public void Persiste()
        {
            try
            {
                DBConnect.AbreConexaoSeNaoEstiverAberta();

                IDbCommand command = DBConnect.Connection.CreateCommand();

                command.CommandText = this.ToInsertQuery();

                command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                DBConnect.Connection.Close();
                throw;
            }
        }
    }
}
