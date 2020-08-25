using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IConfiguracaoPoker
    {
        //Todo: adicionar itens configuracao
        uint Ant { get; }
        uint Flop { get; }
        uint Turn { get; }
        uint River { get; }
    }
}
