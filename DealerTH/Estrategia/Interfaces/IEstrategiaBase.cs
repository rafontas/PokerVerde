using System;
using System.Collections.Generic;
using System.Text;

namespace MaoTH.Estrategia
{
    interface IEstrategiaDescricao
    {
        string DescricaoGeral { get; }
        string PreFlop { get; }
        string Flop { get; }
        string Turn { get; }
        string River { get; }
        string CondicaoDeParada { get; }
    }
}
