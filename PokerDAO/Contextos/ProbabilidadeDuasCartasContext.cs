using PokerDAO.Base;
using PokerDAO.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PokerDAO.Contextos
{
    public static class ProbabilidadeApenasDuasCartasContext
    {
        public static void Persiste(IList<IProbabilidadeMaoInicial> MaosParaPersistir)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                foreach(IProbabilidadeMaoInicial p in MaosParaPersistir)
                {
                    IDbCommand command = DBConnect.Connection.CreateCommand();
                    command.CommandText = ProbabilidadeApenasDuasCartasContext.ToInsertQuery(p);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                DBConnect.FecharConexao();
            }
        }

        public static bool JaExisteProbabilidadeCadastrada(IProbabilidadeMaoInicial MaosParaPersistir) 
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat("SELECT COUNT(*) FROM probabilidade.mao_duas_cartas WHERE " +
                    "numero_carta_1 = {0} AND" + Environment.NewLine +
                    "numero_carta_2 = {1} AND" + Environment.NewLine +
                    "offorsuited = \'{2}\' AND" + Environment.NewLine +
                    "qtd_jogos_simulados = {3}",
                MaosParaPersistir.NumCarta1,
                MaosParaPersistir.NumCarta2,
                MaosParaPersistir.OffOrSuited,
                MaosParaPersistir.QuantidadesJogosSimulados
            );

            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();

                command.CommandText = strBuilder.ToString();
                IDataReader d = command.ExecuteReader();
                d.Read();
                long numeroDeLinhas = (long) d.GetValue(0);
                d.Close();

                return (numeroDeLinhas > 0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public static void Persiste(IProbabilidadeMaoInicial MaosParaPersistir)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();
                command.CommandText = ProbabilidadeApenasDuasCartasContext.ToInsertQuery(MaosParaPersistir);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                DBConnect.FecharConexao();
            }
        }

        private static string ToInsertQuery(IProbabilidadeMaoInicial probabilidadeMaoInicial) 
        {
            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.mao_duas_cartas (" +
                    "numero_carta_1, " + Environment.NewLine +
                    "numero_carta_2, " + Environment.NewLine +
                    "offorsuited, " + Environment.NewLine +
                    "probabilidade, " + Environment.NewLine +
                    "qtd_jogos_simulados " + Environment.NewLine +
                    ")"
            );

            strBuilder.AppendFormat("VALUES (" +
                    "{0}," + Environment.NewLine +
                    "{1}," + Environment.NewLine +
                    "\'{2}\'," + Environment.NewLine +
                    "{3}," + Environment.NewLine +
                    "{4}" + Environment.NewLine +
                ")",
                probabilidadeMaoInicial.NumCarta1,
                probabilidadeMaoInicial.NumCarta2,
                probabilidadeMaoInicial.OffOrSuited,
                probabilidadeMaoInicial.Probabilidade,
                probabilidadeMaoInicial.QuantidadesJogosSimulados
            );

            return strBuilder.ToString();
        }

    }
}
