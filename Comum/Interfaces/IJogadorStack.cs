using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IJogadorStack
    {
        uint StackInicial { get; }
        uint Stack { get; }
        uint PagarValor(uint ValorHaPagar);
        uint ReceberValor(uint ValorHaReceber);
        bool PossoPagarValor(uint Valor);
    }
}
