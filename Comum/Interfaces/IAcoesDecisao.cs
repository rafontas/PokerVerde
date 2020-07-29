using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IAcoesDecisao
    {
        string IdMente { get; }
        int VersaoIdMente { get; }
        ConfiguracaoTHBonus Config { get; }
        void SetStackAgora(uint StackInicial, uint Stack);
        bool PossoPagarValor(uint ValorAhPagar);
        AcaoJogador PreJogo(uint valor);
        AcaoJogador PreFlop(uint valor);
        AcaoJogador Flop(Carta[] cartasMesa, uint valor);
        AcaoJogador Turn(Carta[] cartasMesa, uint valor);
        AcaoJogador River(Carta[] cartasMesa);
        AcaoJogador FimDeJogo();
        AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa);
    }
}
