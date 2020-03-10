using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public interface IJogadorAcoesTHBonus
    {
        ConfiguracaoTHBonus config { set; get; }
        bool TenhoStackParaJogar();
        AcaoJogador PreJogo();
        AcaoJogador PreFlop();
        AcaoJogador Flop(Carta[] cartasMesas);
        AcaoJogador Turn(Carta[] cartasMesas);
        AcaoJogador River(Carta[] cartasMesas);
        AcaoJogador PosRiver(Carta[] cartasMesas);
    }
}
