using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface ISitAndGo
    {
        IConfiguracaoPoker ConfiguracaoPoker { get; }
        IList<IJogador> Jogadores { get; }
        void Executa();
    }
}
