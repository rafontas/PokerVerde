using Comum.Interfaces.PokerBase;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes.Poker
{
    public class MaoTexasHoldem : IMao
    {
        public Carta[] Cartas => throw new NotImplementedException();

        public MaoTexasHoldem (Carta c1, Carta c2) 
        {
        }
            
        public bool Validar() => throw new NotImplementedException();

    }
}
