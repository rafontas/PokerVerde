using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public interface IJogadorAcoes
    {
        AcaoJogador PreJogo(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento);
        AcaoJogador PreFlop(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento);
        AcaoJogador Flop(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento);
        AcaoJogador Turn(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento);
        AcaoJogador River(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento);
        AcaoJogador PosRiver(Carta[] minhasCartas, Carta[] cartasMesas, TipoRodada momento);
    }
}
