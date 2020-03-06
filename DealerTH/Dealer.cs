using Modelo;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DealerTH
{
    public class Dealer : IDealer
    {
        public int JogadorGanhouTHB(IList<Carta> Mesa, Carta[] CartaJogador, Carta[] CartaBanca)
        {
            // Monta a melhor mão do jogador
            MelhorMao jogador = new MelhorMao();
            MaoTexasHoldem MaoJogador = jogador.AvaliaMao(new List<Carta>() { CartaJogador[0], CartaJogador[1],
                Mesa[0], Mesa[1], Mesa[2], Mesa[3], Mesa[4] });

            // Monta a melhor mão da banca
            MelhorMao mesa = new MelhorMao();
            MaoTexasHoldem MaoMesa = mesa.AvaliaMao(new List<Carta>() { CartaBanca[0], CartaBanca[1],
                Mesa[0], Mesa[1], Mesa[2], Mesa[3], Mesa[4] });

            return MaoJogador.Compara(MaoMesa);
        }
    }
}
