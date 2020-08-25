using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.PokerBase
{
    public interface IMao
    {
        Carta [] Cartas { get; }

        bool Validar();
    }
}
