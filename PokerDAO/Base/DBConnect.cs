using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;

namespace PokerDAO.Base
{
    public static class DBConnect
    {
        public static IDbConnection Connection;

        public const string BD_IP = "BD-Ip";
        public const string BD_PORTA = "BD-Port";
        public const string BD_NOMEBANCO = "BD-Name";
        public const string BD_USER_NAME = "BD-Login";
        public const string BD_USER_PASSWORD = "BD-Password";

        public static string Ip { get; set; } = ConfigurationManager.AppSettings.Get(BD_IP);
        public static string Porta { get; set; } = ConfigurationManager.AppSettings.Get(BD_PORTA);
        public static string NomeBanco { get; set; } = ConfigurationManager.AppSettings.Get(BD_NOMEBANCO);
        public static string UserName { get; set; } = ConfigurationManager.AppSettings.Get(BD_USER_NAME);
        public static string UserPassword { get; set; } = ConfigurationManager.AppSettings.Get(BD_USER_PASSWORD);

        public static string ConnectionUrl
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat("Server={0};", Ip);
                strBuilder.AppendFormat("Port={0};", Porta);
                strBuilder.AppendFormat("User Id={0};", UserName);
                strBuilder.AppendFormat("Password={0};", UserPassword);
                strBuilder.AppendFormat("Database={0};", NomeBanco);

                return strBuilder.ToString();
            }
        }

        public static bool EstouConectado()
        {
            if (DBConnect.Connection == null) return false;

            return (DBConnect.Connection.State != ConnectionState.Closed);
        }

        public static void AbreConexaoSeNaoEstiverAberta()
        {
            if (DBConnect.EstouConectado()) return;
            
            try
            {
                DBConnect.Connection = new NpgsqlConnection(ConnectionUrl);
                Connection.Open();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static void FecharConexao()
        {
            if (DBConnect.EstouConectado())
                Connection.Close();
        }
    }
}
