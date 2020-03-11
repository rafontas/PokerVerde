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
        AcaoJogador PreJogo(uint valor);
        AcaoJogador PreFlop(uint valor);
        AcaoJogador Flop(Carta[] cartasMesa, uint valor);
        AcaoJogador Turn(Carta[] cartasMesa, uint valor);
        AcaoJogador River(Carta[] cartasMesa);
        AcaoJogador FimDeJogo();
        AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa);
    }
}
