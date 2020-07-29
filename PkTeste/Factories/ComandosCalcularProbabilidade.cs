using PkTeste.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PkTeste.Factories
{
    public class ComandosCalcularProbabilidade : IPokerComandos
    {

        private Dictionary<string, Comando> comandos = new Dictionary<string, Comando> {
            { "c", new Comando("c", "cancelar e sair") }
        };

        public IDictionary<string, Comando> getComandos() => comandos;

        public string getDesc()
        {
            throw new NotImplementedException();
        }

        public string getHelp()
        {
            throw new NotImplementedException();
        }

        public bool validaComando(Comando cm)
        {
            throw new NotImplementedException();
        }
    }
}
