using Modelo;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DealerTH
{
    public class Dealer : IDealer
    {
        public bool JogadorGanhouTHB(IList<Carta> Mesa, Carta[] CartaJogador, Carta[] CartaBanca)
        {
            throw new NotImplementedException();
        }

        public IList<IJogador> VerificaGanhadores(IList<Carta> Mesa, IList<MaoTexasHoldem> Maos)
        {
            throw new NotImplementedException();
        }

        private IList<IJogo> EncontraMelhorJogo(MaoTexasHoldem mao, IList<Carta> Mesa)
        {
            IList<Carta> CartaOrdenada = Mesa.OrderByDescending(c => c.Numero).ToList();


            return null;
        }
    }
}
