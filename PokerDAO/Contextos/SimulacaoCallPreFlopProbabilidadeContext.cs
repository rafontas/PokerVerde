using Comum.Interfaces.AnaliseProbabilidade;
using PokerDAO.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PokerDAO.Contextos
{
    public static class SimulacaoCallPreFlopProbabilidadeContext
    {
        private static uint qtdJogosPadrao { get; set; } = 500000;

        public static void Persiste(IList<ISimulacaoCallPreFlop> MaosParaPersistir)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                foreach (ISimulacaoCallPreFlop p in MaosParaPersistir)
                {
                    IDbCommand command = DBConnect.Connection.CreateCommand();
                    command.CommandText = SimulacaoCallPreFlopProbabilidadeContext.ToInsertQuery(p);
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
                command.CommandText = SimulacaoCallPreFlopProbabilidadeContext.ToInsertQuery(simulacaoCallPreFlop);
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

        public static bool JaExisteProbabilidadeCadastrada(IMaoBasica mb, uint ? qtdJogosJogados = null)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            uint? jogosJogados = (qtdJogosJogados ?? qtdJogosPadrao);

            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat("" +
                    "SELECT COUNT(*) " +
                    "FROM " +
                        "probabilidade.simulacao_call_pre_flop sc" +
                        "left join probabilidade.mao_duas_cartas mao ON sc.id_mao_duas_cartas = mao.id" +
                    "WHERE " +
                        "numero_carta_1 = {0} AND" + Environment.NewLine +
                        "numero_carta_2 = {1} AND" + Environment.NewLine +
                        "offorsuited = \'{2}\' AND" + Environment.NewLine +
                        "qtd_jogos_simulados = {3}",
                mb.NumCarta1,
                mb.NumCarta2,
                mb.OffOrSuited,
                jogosJogados
            );

            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();

                command.CommandText = strBuilder.ToString();
                IDataReader d = command.ExecuteReader();
                d.Read();
                long numeroDeLinhas = (long)d.GetValue(0);
                d.Close();

                return (numeroDeLinhas > 0);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //private static int GetNextValSequence() { }

        private static string ToInsertQuery(ISimulacaoCallPreFlop simulacaoCallPreFlop) {

            int sequence_id_grupo = 0;

            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.simulacao_call_pre_flop (" +
                            "id_grupo," + Environment.NewLine +
                            "qtd_jogos_simulados, " + Environment.NewLine +
                            "qtd_jogos_simulados_pretendidos, " + Environment.NewLine +
                            "qtd_jogos_ganhos, " + Environment.NewLine +
                            "qtd_jogos_perdidos, " + Environment.NewLine +
                            "qtd_jogos_empatados, " + Environment.NewLine +
                            "raise_flop, " + Environment.NewLine +
                            "raise_flop_turn, " + Environment.NewLine +
                            "val_stack_inicial, " + Environment.NewLine +
                            "val_stack_final, " + Environment.NewLine +
                            "id_mao_duas_cartas)"
            );

            strBuilder.AppendFormat("VALUES (" +
                    "{0}," + Environment.NewLine +
                    "{1}," + Environment.NewLine +
                    "{2}," + Environment.NewLine +
                    "{3}," + Environment.NewLine +
                    "{4}," + Environment.NewLine +
                    "{5}," + Environment.NewLine +
                    "{6}," + Environment.NewLine +
                    "{7}," + Environment.NewLine +
                    "{8}," + Environment.NewLine +
                    "{9}," + Environment.NewLine +
                    "{10}" +
                    ")",
                simulacaoCallPreFlop.IdGrupo,
                simulacaoCallPreFlop.QuantidadeJogosSimulados,
                simulacaoCallPreFlop.QuantidadeJogosSimuladosPretendidos,
                simulacaoCallPreFlop.QuantidadeJogosGanhos,
                simulacaoCallPreFlop.QuantidadeJogosPerdidos,
                simulacaoCallPreFlop.QuantidadeJogosEmpatados,
                simulacaoCallPreFlop.RaiseFlop.ToString(),
                simulacaoCallPreFlop.RaiseFlopTurn.ToString(),
                simulacaoCallPreFlop.StackInicial,
                simulacaoCallPreFlop.StackFinal,
                simulacaoCallPreFlop.ProbabilidadeMaoInicial.Id
            );

            return strBuilder.ToString();
        }

    }
}
