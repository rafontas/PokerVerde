using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IAcoesDecisao
    {
        string idMente { get; }
        int versaoIdMente { get; }
        ConfiguracaoTHBonus config { get; }
        AcaoJogador PreJogo(uint valor);
        AcaoJogador PreFlop(uint valor);
        AcaoJogador Flop(Carta[] cartasMesa, uint valor);
        AcaoJogador Turn(Carta[] cartasMesa, uint valor);
        AcaoJogador River(Carta[] cartasMesa);
        AcaoJogador FimDeJogo();
        AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa);
    }
}
