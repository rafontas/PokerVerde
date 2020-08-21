using Comum.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace PkTeste.Interfaces
{
    public interface IBuilderSitAndGo
    {
        IBuilderSitAndGo addJogador(uint quantidadePartidasExecutar);
        IBuilderSitAndGo addJogador(IJogador jogador);
        IBuilderSitAndGo SetRestantePadrao();
        ISitAndGo GetResult();
    }
}
