using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public enum Naipe {
        Copas,
        Ouros,
        Paus,
        Espadas
    }

    public enum TipoPoker
    {
        TexasHoldem
    }

    public enum TipoAcao
    {
        Check,
        Call,
        Raise,
        Play

    }

    public enum MomentoJogo
    {
        PreJogo,
        PreFlop,
        PreTurn,
        PreRiver,
        River
    }

}
