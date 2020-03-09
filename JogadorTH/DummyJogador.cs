using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public class DummyJogador : JogadorBase
    {
        public AcaoJogador Flop(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento)
        {
            throw new NotImplementedException();
        }

        public AcaoJogador PosRiver(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento)
        {
            throw new NotImplementedException();
        }

        public AcaoJogador PreFlop(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento)
        {
            throw new NotImplementedException();
        }

        public AcaoJogador PreJogo(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento)
        {
            throw new NotImplementedException();
        }

        public AcaoJogador River(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento)
        {
            throw new NotImplementedException();
        }

        public AcaoJogador Turn(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento)
        {
            throw new NotImplementedException();
        }
    }
}
