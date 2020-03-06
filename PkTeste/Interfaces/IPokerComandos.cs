using System;
using System.Collections.Generic;
using System.Text;

namespace PkTeste.Interfaces
{
    public interface IPokerComandos
    {
        string getHelp();
        string getDesc();
        bool validaComando(Comando cm);
        IDictionary<string, Comando> getComandos();
    }
}
