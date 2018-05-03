using Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    /// <summary>
    /// Contém informação sobre o jogo no geral.
    /// </summary>
    public interface EstadoJogo<T>
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
