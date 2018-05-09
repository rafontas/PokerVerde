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
            //IList<Carta> cartasJogador = new List<Carta>();
            //cartasJogador = cartasJogador.Concat(CartaJogador.ToList()).ToList();
            //cartasJogador = cartasJogador.Concat(Mesa).ToList();
            //MaoChecagemJogador = new MaoChecagem(cartasJogador);

            //IList<Carta> cartasBanca = new List<Carta>();
            //cartasBanca = cartasBanca.Concat(CartaBanca.ToList()).ToList();
            //cartasBanca = cartasBanca.Concat(Mesa).ToList();
            //MaoChecagemMesa = new MaoChecagem(cartasBanca);

            return true;
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
