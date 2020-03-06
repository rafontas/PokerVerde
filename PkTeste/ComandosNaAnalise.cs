using PkTeste.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PkTeste
{
    public class ComandosNaAnalise : IPokerComandos
    {

        static string pokerPausado = @"
                -cls : limpa o console;
                -e : cancelar e sair do programa;
                -p : próximo passo;
                -r : repetir a rodada anterior;
                -m [Número rodada]: repete uma rodada específica";

        private Dictionary<string, Comando> comandos = new Dictionary<string, Comando> {
                { "e", new Comando("e", "cancelar e sair") },
                { "p", new Comando("p", "próximo passo") },
                { "r", new Comando("r", "repetir rodada anterior") },
                { "m", new Comando("m", "repete uma rodada específica") },
                { "cls", new Comando("cls", "limpar o console") },
            };

        public string getHelp()
        {
            string help = @"Bem vindo ao Poker Verde.As opções são:" + Environment.NewLine;

            foreach (var c in this.comandos)
                help += String.Format("\t-{0}{1}", c.Value.getDesc(), Environment.NewLine);

            return help;
        }

        public bool validaComando(Comando cm) => this.comandos.ContainsKey(cm.id);

        public string getDesc() => @"Comandos relativos ao uso geral.";

        public IDictionary<string, Comando> getComandos() => this.comandos;

    }
}
