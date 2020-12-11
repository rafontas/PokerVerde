using Comum.Interfaces.AnaliseProbabilidade;
using PokerDAO.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PokerDAO.Contextos
{
    public static class SimulacaoJogosResumoContext
    {
        public static void Persiste(IList<ISimulacaoJogosResumo> MaosParaPersistir)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                foreach (ISimulacaoJogosResumo p in MaosParaPersistir)
                {
                    IDbCommand command = DBConnect.Connection.CreateCommand();
                    command.CommandText = SimulacaoJogosResumoContext.ToInsertQuery(p);
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

        public static void Persiste(ISimulacaoJogosResumo simulacaoCallPreFlop)
        {
            if(!AcaoProbabilidadeContexto.ExisteItem(simulacaoCallPreFlop.AcaoProbabilidade))
                AcaoProbabilidadeContexto.Persiste(simulacaoCallPreFlop.AcaoProbabilidade);

            simulacaoCallPreFlop.AcaoProbabilidade.id = 
                AcaoProbabilidadeContexto.GetItem(
                    simulacaoCallPreFlop.AcaoProbabilidade.probabilidadeMinicaSeeFlop,
                    simulacaoCallPreFlop.AcaoProbabilidade.probabilidadeMinimaRaisePreTurn,
                    simulacaoCallPreFlop.AcaoProbabilidade.probabilidadeMinimaRaisePreRiver).id;

            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();
                command.CommandText = SimulacaoJogosResumoContext.ToInsertQuery(simulacaoCallPreFlop);
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

        private static string ToInsertQuery(ISimulacaoJogosResumo simulacaoCallPreFlop) {

            int sequence_id_grupo = 0;

            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.tb_simulacao_jogos_resumo (" +
                            "val_stack_inicial, " + Environment.NewLine +
                            "val_stack_final, " + Environment.NewLine +
                            "qtd_jogos_simulados, " + Environment.NewLine +
                            "qtd_jogos_simulados_pretendidos, " + Environment.NewLine +
                            "qtd_jogos_ganhos, " + Environment.NewLine +
                            "qtd_jogos_perdidos, " + Environment.NewLine +
                            "qtd_jogos_empatados, " + Environment.NewLine +
                            "ds_inteligencia"
                            
            );

            if (simulacaoCallPreFlop.AcaoProbabilidade != null)
            {
                strBuilder.Append(", id_acao_probabilidade" + Environment.NewLine);
            }

            strBuilder.Append(") " );

            strBuilder.AppendFormat("VALUES (" +
                    "{0}," + Environment.NewLine +
                    "{1}," + Environment.NewLine +
                    "{2}," + Environment.NewLine +
                    "{3}," + Environment.NewLine +
                    "{4}," + Environment.NewLine +
                    "{5}," + Environment.NewLine +
                    "{6}," + Environment.NewLine +
                    "\'{7}\'",
                simulacaoCallPreFlop.StackInicial,
                simulacaoCallPreFlop.StackFinal,
                simulacaoCallPreFlop.QuantidadeJogosSimulados,
                simulacaoCallPreFlop.QuantidadeJogosSimuladosPretendidos,
                simulacaoCallPreFlop.QuantidadeJogosGanhos,
                simulacaoCallPreFlop.QuantidadeJogosPerdidos,
                simulacaoCallPreFlop.QuantidadeJogosEmpatados,
                simulacaoCallPreFlop.DescricaoInteligencia
            );

            if (simulacaoCallPreFlop.AcaoProbabilidade != null)
            {
                strBuilder.AppendFormat(", {0} ", simulacaoCallPreFlop.AcaoProbabilidade.id);
            }

            strBuilder.Append(")");

            return strBuilder.ToString();
        }
        
    }
}
