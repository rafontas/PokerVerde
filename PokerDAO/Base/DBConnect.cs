using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;

namespace PokerDAO.Base
{
    public class DBConnect
    {
        public IDbConnection Connection { get; private set; }

        protected const string BD_IP = "BD-Ip";
        protected const string BD_PORTA = "BD-Port";
        protected const string BD_NOMEBANCO = "BD-Name";
        protected const string BD_USER_NAME = "BD-Login";
        protected const string BD_USER_PASSWORD = "BD-Password";

        private string Ip { get; set; }
        private string Porta { get; set; }
        private string NomeBanco { get; set; }
        private string UserName { get; set; }
        private string UserPassword { get; set; }
        
        private string ConnectionUrl
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat("Server={0};", this.Ip);
                strBuilder.AppendFormat("Port={0};", this.Porta);
                strBuilder.AppendFormat("User Id={0};", this.UserName);
                strBuilder.AppendFormat("Password={0};", this.UserPassword);
                strBuilder.AppendFormat("Database={0};", this.NomeBanco);

                return strBuilder.ToString();
            }
        }

        public bool EstouConectado()
        {
            bool ConexaoEstaAberta = true;

            try
            {
                string sql = "SELECT version()";
                IDbCommand cmd = new NpgsqlCommand(sql, (NpgsqlConnection) this.Connection);
                var version = cmd.ExecuteScalar().ToString();
            }
            catch(Exception e)
            {
                return false;
            }

            return ConexaoEstaAberta;
        }

        public void AbreConexao()
        {
            if (this.Connection?.State == ConnectionState.Open) return;
            
            try
            {
                this.Connection = new NpgsqlConnection(this.ConnectionUrl);
                this.Connection.Open();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void FecharConexao() => this.Connection.Close();

        public DBConnect()
        {
            this.Ip = ConfigurationManager.AppSettings.Get(BD_IP);
            this.Porta = ConfigurationManager.AppSettings.Get(BD_PORTA);
            this.NomeBanco = ConfigurationManager.AppSettings.Get(BD_NOMEBANCO);
            this.UserName = ConfigurationManager.AppSettings.Get(BD_USER_NAME);
            this.UserPassword = ConfigurationManager.AppSettings.Get(BD_USER_PASSWORD);
        }
    }
}
