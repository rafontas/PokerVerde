using Modelo;
using System.Collections.Generic;

namespace Modelo
{
    public interface IDealer
    {
        /// <summary>
        /// Lista de jogadores que ganharam a partida.
        /// </summary>
        /// <param name="Mesa">As cartas que estão na mesa</param>
        /// <param name="Maos"></param>
        /// <returns></returns>
        IList<IJogador> VerificaGanhadores(IList<Carta> Mesa, IList<MaoTexasHoldem> Maos);

        /// <summary>
        /// Verifica se o jogador ganhou da mesa no Texas Holdem Bonus.
        /// </summary>
        /// <param name="Mesa">Cartas da mesa</param>
        /// <param name="CartaJogador">As cartas do Jogador</param>
        /// <param name="CartaBanca">Cartas da Banca</param>
        /// <returns></returns>
        bool JogadorGanhouTHB(IList<Carta> Mesa, Carta[] CartaJogador, Carta[] CartaBanca);
    }
}
