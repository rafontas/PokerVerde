using System;
using System.Collections.Generic;
using System.Text;
using Modelo;

namespace Comum.Interfaces
{
    public interface IPartida
    {
        IDictionary<uint, IJogador> Jogadores { get; }
        IList<IRodada> Rodadas { get;  }
        uint NumeroDaPartida { get; }
        uint QuantidadeJogadores { get; }
        uint IdVitorioso { get; }
        uint TotalPot { get; }
    }
}
