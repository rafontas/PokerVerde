using Comum.Interfaces.AnaliseProbabilidade;
using PokerDAO.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PokerDAO.Contextos
{
    public static class SimulacaoCallPreFlopContext
    {
        public static void Persiste(IList<ISimulacaoCallPreFlop> MaosParaPersistir)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                foreach (ISimulacaoCallPreFlop p in MaosParaPersistir)
                {
                    IDbCommand command = DBConnect.Connection.CreateCommand();
                    command.CommandText = SimulacaoCallPreFlopContext.ToInsertQuery(p);
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

        public static void Persiste(ISimulacaoCallPreFlop simulacaoCallPreFlop)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();
                command.CommandText = SimulacaoCallPreFlopContext.ToInsertQuery(simulacaoCallPreFlop);
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

        private static string ToInsertQuery(ISimulacaoCallPreFlop simulacaoCallPreFlop) {

            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.simulacao_call_pre_flop (" +
                            "qtd_jogos_simulados, " + Environment.NewLine +
                            "val_stack_inicial, " + Environment.NewLine +
                            "val_stack_final, " + Environment.NewLine +
                            "id_mao_duas_cartas)"
            );

            strBuilder.AppendFormat("VALUES (" +
                    "{0}," + Environment.NewLine +
                    "{1}," + Environment.NewLine +
                    "{2}," + Environment.NewLine +
                    "{3}" +
                    ")",
                simulacaoCallPreFlop.QuantidadesJogosSimulados,
                simulacaoCallPreFlop.StackInicial,
                simulacaoCallPreFlop.StackInicial,
                simulacaoCallPreFlop.ProbabilidadeMaoInicial.Id
            );

            return strBuilder.ToString();
        }

    }
}
