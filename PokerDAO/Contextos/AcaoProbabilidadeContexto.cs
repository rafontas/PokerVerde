using Comum.Interfaces.AnaliseProbabilidade;
using Comum.Classes.Poker.AnaliseProbabilidade;
using PokerDAO.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PokerDAO.Contextos
{
    public class AcaoProbabilidadeContexto
    {
        public static void Persiste(IList<IAcaoProbabilidade> acaoProbabilidade)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                foreach (IAcaoProbabilidade p in acaoProbabilidade)
                {
                    IDbCommand command = DBConnect.Connection.CreateCommand();
                    command.CommandText = AcaoProbabilidadeContexto.ToInsertQuery(p);
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

        public static void Persiste(IAcaoProbabilidade simulacaoCallPreFlop)
        {
            if (simulacaoCallPreFlop == null) return;

            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();
                command.CommandText = AcaoProbabilidadeContexto.ToInsertQuery(simulacaoCallPreFlop);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                DBConnect.FecharConexao();
            }
        }

        private static IAcaoProbabilidade GetItem (IDataReader dataReader)
        {
            int index = 0;

            IAcaoProbabilidade item = new AcaoProbabilidade();

            item.id = (int)dataReader.GetInt32(index++);
            item.probabilidadeMinicaSeeFlop = (float)dataReader.GetFloat(index++);
            item.probabilidadeMinimaRaisePreTurn = (float)dataReader.GetFloat(index++);
            item.probabilidadeMinimaRaisePreRiver = (float)dataReader.GetFloat(index++);

            return item;
        }

        public static bool ExisteItem(IAcaoProbabilidade acao)
        {
            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat("SELECT count(*) FROM probabilidade.acao_probabilidade WHERE " +
                            "val_call_pre_flop = {0}" + Environment.NewLine +
                            "AND val_raise_pre_turn = {1}" + Environment.NewLine +
                            "AND val_raise_pre_river = {2}" + Environment.NewLine,
                            acao.probabilidadeMinicaSeeFlop.ToString(),
                            acao.probabilidadeMinimaRaisePreTurn.ToString(),
                            acao.probabilidadeMinimaRaisePreRiver.ToString()
            );

            try
            {
                DBConnect.AbreConexaoSeNaoEstiverAberta();

                IDbCommand command = DBConnect.Connection.CreateCommand();

                command.CommandText = strBuilder.ToString();
                IDataReader d = command.ExecuteReader();
                d.Read();
                long numeroDeLinhas = (long) d.GetValue(0);
                d.Close();

                DBConnect.FecharConexao();

                return (numeroDeLinhas > 0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static IAcaoProbabilidade GetItem(float callPreFlop, float raisePreTurn, float raisePreRiver) 
        {
            IAcaoProbabilidade acaoProbailidade = null;

            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat("SELECT * FROM probabilidade.acao_probabilidade WHERE " +
                            "val_call_pre_flop = {0}" + Environment.NewLine +
                            "AND val_raise_pre_turn = {1}" + Environment.NewLine +
                            "AND val_raise_pre_river = {2}",
                            callPreFlop.ToString(),
                            raisePreTurn.ToString(),
                            raisePreRiver.ToString()
            );

            DBConnect.AbreConexaoSeNaoEstiverAberta();
            IDbCommand command = DBConnect.Connection.CreateCommand();
            command.CommandText = strBuilder.ToString();

            using (IDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                    acaoProbailidade = AcaoProbabilidadeContexto.GetItem(dataReader);
            }

            DBConnect.FecharConexao();

            return acaoProbailidade;

        }

        public static int UltimoPersistido()
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat("SELECT currval('acao_probabilidade_id_seq')");

            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();

                command.CommandText = strBuilder.ToString();
                IDataReader d = command.ExecuteReader();
                d.Read();
                int numeroUltimoSeq = (int)d.GetValue(0);
                d.Close();

                return numeroUltimoSeq;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string ToInsertQuery(IAcaoProbabilidade acaoProbabilidade)
        {

            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.acao_probabilidade (" +
                            "val_call_pre_flop, " + Environment.NewLine +
                            "val_raise_pre_turn, " + Environment.NewLine +
                            "val_raise_pre_river " + Environment.NewLine +
                            ")" + Environment.NewLine
            );

            strBuilder.AppendFormat("VALUES (" +
                    "{0}," + Environment.NewLine +
                    "{1}," + Environment.NewLine +
                    "{2}" + Environment.NewLine +
                    ")",
                acaoProbabilidade.probabilidadeMinicaSeeFlop,
                acaoProbabilidade.probabilidadeMinimaRaisePreTurn,
                acaoProbabilidade.probabilidadeMinimaRaisePreRiver
            );

            return strBuilder.ToString();
        }
    }
}
