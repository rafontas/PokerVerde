using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.PokerBase
{
    public interface IBuilderSitAndGo
    {
        IBuilderSitAndGo addJogador(IJogador jogador);
        IBuilderSitAndGo SetBanca(IJogador jogador);
        ISitAndGo ToSitAndGo();
    }
}
