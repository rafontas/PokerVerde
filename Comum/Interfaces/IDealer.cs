using Modelo;
using System.Collections.Generic;

namespace Modelo
{
    public interface IDealer
    {
        /// <summary>
        /// Verifica se o jogador ganhou da mesa no Texas Holdem Bonus.
        /// </summary>
        /// <param name="Mesa">Cartas da mesa</param>
        /// <param name="CartaJogador">As cartas do Jogador</param>
        /// <param name="CartaBanca">Cartas da Banca</param>
        /// <returns>1 - Vitória, 2 - Derrota, 0 - Empate</returns>
        int JogadorGanhouTHB(IList<Carta> Mesa, Carta[] CartaJogador, Carta[] CartaBanca);
    }
}
