using Comum.Classes;
using Comum.Interfaces;
using Comum.Interfaces.AnaliseProbabilidade;
using PokerDAO.Base;
using PokerDAO.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PokerDAO.Contextos
{
    public static class ProbabilidadeMaoInicialContext
    {
        public static void Persiste(IList<IProbabilidadeMaoInicial> MaosParaPersistir)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                foreach(IProbabilidadeMaoInicial p in MaosParaPersistir)
                {
                    IDbCommand command = DBConnect.Connection.CreateCommand();
                    command.CommandText = ProbabilidadeMaoInicialContext.ToInsertQuery(p);
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
                command.CommandText = ProbabilidadeMaoInicialContext.ToInsertQuery(MaosParaPersistir);
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

        public static void AtualizaPorNumerosOffOuSuitedQtdJogosSimulados(IList<IProbabilidadeMaoInicial> MaosParaPersistir)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                foreach (IProbabilidadeMaoInicial p in MaosParaPersistir)
                {
                    IDbCommand command = DBConnect.Connection.CreateCommand();
                    command.CommandText = ProbabilidadeMaoInicialContext.ToUpdateProbabilidadeSairQuery(p);
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

        private static string ToUpdateProbabilidadeSairQuery(IProbabilidadeMaoInicial probabilidadeMaoInicial)
        {
            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat("UPDATE probabilidade.mao_duas_cartas SET" + Environment.NewLine +
                    "probabilidade_sair = {0}" + Environment.NewLine +
                    "WHERE " + Environment.NewLine +
                    "  numero_carta_1 = {1}" + Environment.NewLine +
                    "AND  numero_carta_2 = {2}" + Environment.NewLine +
                    "AND  offorsuited = \'{3}\'" + Environment.NewLine +
                    "AND  qtd_jogos_simulados = {4}" + Environment.NewLine,
                probabilidadeMaoInicial.ProbabilidadeSair,
                probabilidadeMaoInicial.NumCarta1,
                probabilidadeMaoInicial.NumCarta2,
                probabilidadeMaoInicial.OffOrSuited,
                probabilidadeMaoInicial.QuantidadesJogosSimulados
            );

            return strBuilder.ToString();
        }
        
        public static IList<IProbabilidadeMaoInicial> GetMaosProbabilidadesIniciais(int qtd_jogos_simulados = 500000)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            StringBuilder strBuilder 
                = ProbabilidadeMaoInicialContext.GetSelectQuery()
                    .AppendFormat(" WHERE m.qtd_jogos_simulados = {0}",
                        qtd_jogos_simulados
                    );

            IDbCommand command = DBConnect.Connection.CreateCommand();
            command.CommandText = strBuilder.ToString();

            IList<IProbabilidadeMaoInicial> probabilidadeMaoIniciais = new List<IProbabilidadeMaoInicial>();

            using (IDataReader dataReader = command.ExecuteReader())
            {
                while(dataReader.Read())
                {
                    probabilidadeMaoIniciais.Add(ProbabilidadeMaoInicialContext.GetItem(dataReader));
                }
            }

            return probabilidadeMaoIniciais;
        }

        private static StringBuilder GetSelectQuery()
        {
            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat("SELECT " +
                    "m.id," + Environment.NewLine +
                    "m.numero_carta_1," + Environment.NewLine +
                    "m.numero_carta_2," + Environment.NewLine +
                    "m.offorsuited," + Environment.NewLine +
                    "m.probabilidade_vitoria," + Environment.NewLine +
                    "m.qtd_jogos_simulados," + Environment.NewLine +
                    "m.probabilidade_sair " + Environment.NewLine +
                " FROM probabilidade.mao_duas_cartas m");

            return strBuilder;
        }

        private static IProbabilidadeMaoInicial GetItem(IDataReader dataReader)
        {
            int index = 0;

            if (!dataReader.Read()) throw new Exception("Não há itens a serem lidos.");

            IProbabilidadeMaoInicial item = new ProbabilidadeMaoInicial(dataReader.GetInt32(index++));

            item.NumCarta1 = (uint)dataReader.GetInt32(index++);
            item.NumCarta2 = (uint)dataReader.GetInt32(index++);
            item.OffOrSuited = dataReader.GetChar(index++);

            if (!dataReader.IsDBNull(index))
            {
                item.ProbabilidadeVitoria = dataReader.GetFloat(index++);
            }

            item.QuantidadesJogosSimulados = (uint)dataReader.GetInt32(index++);

            if (!dataReader.IsDBNull(index))
            {
                item.ProbabilidadeSair = dataReader.GetFloat(index++);
            }

            return item;
        }

        public static IProbabilidadeMaoInicial GetItem(IMaoBasica mao, int qtdJogosSimulacao = 500000)
        {
            IProbabilidadeMaoInicial probMaoInicial;
            StringBuilder strSelect = ProbabilidadeMaoInicialContext.GetSelectQuery();

            strSelect.AppendFormat(" WHERE " + Environment.NewLine +
                    "(" + Environment.NewLine +
                    "   (m.numero_carta_1 = {0} AND m.numero_carta_2 = {1}) OR" + Environment.NewLine +
                    "   (m.numero_carta_2 = {2} AND m.numero_carta_1 = {3})" + Environment.NewLine +
                    ") AND " + Environment.NewLine +
                    "m.offorsuited like \'{4}\' " + Environment.NewLine,
                    mao.NumCarta1,
                    mao.NumCarta2,
                    mao.NumCarta1,
                    mao.NumCarta2,
                    mao.OffOrSuited
            );

            DBConnect.AbreConexaoSeNaoEstiverAberta();
            IDbCommand command = DBConnect.Connection.CreateCommand();
            command.CommandText = strSelect.ToString();

            using (IDataReader dataReader = command.ExecuteReader())
            {
                probMaoInicial = ProbabilidadeMaoInicialContext.GetItem(dataReader);
            }

            return probMaoInicial;
        }

        private static string ToInsertQuery(IProbabilidadeMaoInicial probabilidadeMaoInicial) 
       {
            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.mao_duas_cartas (" +
                    "numero_carta_1, " + Environment.NewLine +
                    "numero_carta_2, " + Environment.NewLine +
                    "offorsuited, " + Environment.NewLine +
                    "probabilidade_vitoria, " + Environment.NewLine +
                    "qtd_jogos_simulados, " + Environment.NewLine +
                    "probabilidade_sair" + Environment.NewLine +
                    ")"
            );

            strBuilder.AppendFormat("VALUES (" +
                    "{0}," + Environment.NewLine +
                    "{1}," + Environment.NewLine +
                    "\'{2}\'," + Environment.NewLine +
                    "{3}," + Environment.NewLine +
                    "{4}," + Environment.NewLine +
                    "{5}" + Environment.NewLine +
                ")",
                probabilidadeMaoInicial.NumCarta1,
                probabilidadeMaoInicial.NumCarta2,
                probabilidadeMaoInicial.OffOrSuited,
                probabilidadeMaoInicial.ProbabilidadeVitoria,
                probabilidadeMaoInicial.QuantidadesJogosSimulados,
                probabilidadeMaoInicial.ProbabilidadeSair
            );

            return strBuilder.ToString();
        }

    }
}
