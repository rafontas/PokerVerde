using MesaTh.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MesaTh
{
    /// <summary>
    /// Contém informação sobre o jogo no geral.
    /// </summary>
    public interface IEstadoJogo<T>
    {
        int QuantJogadores();

        int QuantFichas(IJogador Jogador);

        T MomentoJogo();

        int QuantosFalamAntes();

        int QuantosFalamDepois();

        int ValorBigBlind();

        int ValorSmallBlind();

        int ValorAnte();
    }
}
