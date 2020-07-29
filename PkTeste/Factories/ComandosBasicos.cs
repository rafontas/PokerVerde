using System;
using System.Collections.Generic;
using System.Text;
using PkTeste.Interfaces;

namespace PkTeste
{
    public class ComandosBasicos : IPokerComandos
    {

        private Dictionary<string, Comando> comandos = new Dictionary<string, Comando> {
                { "e", new Comando("e", "sair") },
                { "i", new Comando("i", "iniciar o jogo texas hold'em bônus;") },
                { "t", new Comando("t", "iniciar o jogo teste, pausadamente;") },
                { "tr", new Comando("tr", "iniciar o jogo teste, pausado por rodadas, seguido do número de rodadas;") },
                { "cls", new Comando("cls", "limpar o console;") },
                { "s", new Comando("s", "mostrar o status do jogo no momento;") },
            };

        public string getHelp() { 
            string help = @"Bem vindo ao Poker Verde.As opções são:" + Environment.NewLine;

            foreach(var c in this.comandos)
                help += String.Format("\t-{0}{1}", c.Value.getDesc(), Environment.NewLine);

            return help;
        }

        public bool validaComando(Comando cm) => this.comandos.ContainsKey(cm.id);

        public string getDesc() => @"Comandos relativos ao uso geral.";

        public IDictionary<string, Comando> getComandos() => this.comandos;
    }

}
