using Comum.Classes.Poker.AnaliseProbabilidade;
using Comum.Interfaces.AnaliseProbabilidade;
using PokerDAO.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace PokerDAO.Contextos
{
    public class MaoProbabilidadeContexto
    {
        private static IList<IMaoProbabilidade> ListaPersistencia { get; set; } = new List<IMaoProbabilidade>();
        public static int QuantiadeItensMultiplosInsert { get; set; } = 50;
        public static bool InserirAgora { get; set; } = false;
        public static bool PersistirMultiplos { get; set; } = true;

        public static void PersisteItensRestantes()
        {
            if (!MaoProbabilidadeContexto.PersistirMultiplos) return;

            MaoProbabilidadeContexto.InserirAgora = true;
            MaoProbabilidadeContexto.ConstrolaInsercaoMultipla();
        }

        private static void ConstrolaInsercaoMultipla(IMaoProbabilidade maoProbabilidade = null)
        {
            if (maoProbabilidade != null)
                MaoProbabilidadeContexto.ListaPersistencia.Add(maoProbabilidade);

            if (MaoProbabilidadeContexto.ListaPersistencia.Count < MaoProbabilidadeContexto.QuantiadeItensMultiplosInsert &&
                !MaoProbabilidadeContexto.InserirAgora) return;
            try
            {
                MaoProbabilidadeContexto.Persiste(MaoProbabilidadeContexto.ListaPersistencia);
            }
            catch (Exception e)
            {
                int i = 0;
            }
            finally
            {
                MaoProbabilidadeContexto.InserirAgora = false;
                MaoProbabilidadeContexto.ListaPersistencia = new List<IMaoProbabilidade>();
            }
        }

        public static void Persiste(IList<IMaoProbabilidade> acaoProbabilidade)
        {
            DBConnect.AbreConexaoSeNaoEstiverAberta();

            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();
                command.CommandText = MaoProbabilidadeContexto.InsertMultipleQuery(acaoProbabilidade);
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

        public static void Persiste(IMaoProbabilidade maoProbabilidade)
        {
            if (maoProbabilidade == null) return;

            DBConnect.AbreConexaoSeNaoEstiverAberta();

            if (MaoProbabilidadeContexto.PersistirMultiplos)
            {
                MaoProbabilidadeContexto.ConstrolaInsercaoMultipla(maoProbabilidade);
                return;
            }
            try
            {
                IDbCommand command = DBConnect.Connection.CreateCommand();
                command.CommandText = MaoProbabilidadeContexto.ToInsertQuery(maoProbabilidade);
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

        private static string GetSelectString()
            => "SELECT * FROM probabilidade.tb_probabilidade_mao_vencer";

        public static IList<IMaoProbabilidade> GetAllItems()
        {
            IList<IMaoProbabilidade> lista = new List<IMaoProbabilidade>();
            IList<IMaoProbabilidade> novaLista;
            int passo = 100000;

            DBConnect.AbreConexaoSeNaoEstiverAberta();

            int getCount = MaoProbabilidadeContexto.GetQuantidadeItensPersistidos();

            for (int i = 0; (i*passo) < getCount; i++)
            {
                novaLista = MaoProbabilidadeContexto.GetAllItems(passo, i*passo);
                lista = lista.Concat(novaLista).ToList();
            }

            DBConnect.FecharConexao();

            return lista;
        }

        public static int GetQuantidadeItensPersistidos()
        {
            string query = "SELECT COUNT(*) FROM probabilidade.tb_probabilidade_mao_vencer";

            try
            {
                DBConnect.AbreConexaoSeNaoEstiverAberta();

                IDbCommand command = DBConnect.Connection.CreateCommand();

                command.CommandText = query;
                IDataReader d = command.ExecuteReader();
                d.Read();
                long numeroDeLinhas = (long)d.GetValue(0);
                d.Close();

                return (int) numeroDeLinhas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static IList<IMaoProbabilidade> GetAllItems(int numItens, int offset, string clausulaWhere = "")
        {
            IList<IMaoProbabilidade> lista = new List<IMaoProbabilidade>();
            StringBuilder stringBuilder = new StringBuilder(MaoProbabilidadeContexto.GetSelectString());

            stringBuilder.AppendFormat(clausulaWhere);
            stringBuilder.AppendFormat(" LIMIT {0} OFFSET {1}", numItens, offset);

            IDbCommand command = DBConnect.Connection.CreateCommand();
            command.CommandText = stringBuilder.ToString();

            using (IDataReader dataReader = command.ExecuteReader())
            {
                while(dataReader.Read())
                    lista.Add(MaoProbabilidadeContexto.GetItem(dataReader));
            }

            return lista;
        }

        public static IMaoProbabilidade GetItem(string ds_mao_persistida)
        {
            IMaoProbabilidade acaoProbailidade = null;

            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat(MaoProbabilidadeContexto.GetSelectString() +
                            " WHERE " +
                            "ds_jogo_mao = \'{0}\'" + Environment.NewLine,
                            ds_mao_persistida
            );

            DBConnect.AbreConexaoSeNaoEstiverAberta();
            IDbCommand command = DBConnect.Connection.CreateCommand();
            command.CommandText = strBuilder.ToString();

            using (IDataReader dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                    acaoProbailidade = MaoProbabilidadeContexto.GetItem(dataReader);
            }

            DBConnect.FecharConexao();

            return acaoProbailidade;

        }

        public static IList<IMaoProbabilidade> GetHandLike(string maoWhere)
        {
            IList<IMaoProbabilidade> maoProbailidade = new List<IMaoProbabilidade>();

            StringBuilder strBuilder = new StringBuilder()
                .AppendFormat(MaoProbabilidadeContexto.GetSelectString() +
                            " WHERE " +
                            "ds_jogo_mao like \'{0}%\'" + Environment.NewLine,
                            maoWhere
            );

            DBConnect.AbreConexaoSeNaoEstiverAberta();
            IDbCommand command = DBConnect.Connection.CreateCommand();
            command.CommandText = strBuilder.ToString();
            int contador = 0;

            using (IDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    maoProbailidade.Add(MaoProbabilidadeContexto.GetItem(dataReader));
                }
            }

            DBConnect.FecharConexao();

            return maoProbailidade;
        }


        public static IMaoProbabilidade GetItem (IDataReader dataReader)
        {
            int index = 0;

            IMaoProbabilidade item = new MaoProbabilidade(
                (int)dataReader.GetInt32(index++),
                dataReader.GetString(index++),
                dataReader.GetFloat(index++)
            );

            return item;
        }

        private static string InsertMultipleQuery(IList<IMaoProbabilidade> maoProbabilidade)
        {
            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.tb_probabilidade_mao_vencer (" +
                            "ds_jogo_mao, " + Environment.NewLine +
                            "val_prob_vencer " + Environment.NewLine +
                            ") VALUES " + Environment.NewLine
            );

            for(int i = 0; i < maoProbabilidade.Count; i++)
            {
                if (i > 0) strBuilder.Append(", ");
                strBuilder.AppendFormat("( " +
                        "\'{0}\'," + Environment.NewLine +
                        "{1}" + Environment.NewLine +
                        ")",
                    maoProbabilidade[i].ToMaoTokenizada(),
                    maoProbabilidade[i].ProbabilidadeVitoria.ToString("0.0000").Replace(",", ".")
                );
            }

            strBuilder.Append(";");

            return strBuilder.ToString();
        }

        private static string ToInsertQuery(IMaoProbabilidade maoProbabilidade)
        {

            StringBuilder strBuilder = new StringBuilder()
                .Append("INSERT INTO probabilidade.tb_probabilidade_mao_vencer (" +
                            "ds_jogo_mao, " + Environment.NewLine +
                            "val_prob_vencer " + Environment.NewLine +
                            ")" + Environment.NewLine
            );

            strBuilder.AppendFormat("VALUES ( " +
                    "\'{0}\'," + Environment.NewLine +
                    "{1}" + Environment.NewLine +
                    ")",
                maoProbabilidade.ToMaoTokenizada(),
                maoProbabilidade.ProbabilidadeVitoria.ToString("0.0000").Replace(",",".")
            );

            return strBuilder.ToString();
        }
    }
}
