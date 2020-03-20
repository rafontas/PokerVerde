using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH.Acoes
{
    public abstract class AcaoDecisoriaBase : IAcoesDecisao
    {
        public abstract string idMente { get; }

        public abstract int versaoIdMente { get; }

        public abstract ConfiguracaoTHBonus config { get; }

        public abstract AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa);

        public abstract AcaoJogador PreJogo(uint valor);

        public abstract AcaoJogador PreFlop(uint valor);

        public abstract AcaoJogador Flop(Carta[] cartasMesa, uint valor);

        public abstract AcaoJogador River(Carta[] cartasMesa);

        public abstract AcaoJogador FimDeJogo();

        public abstract bool TenhoStackParaJogar();

        public abstract AcaoJogador Turn(Carta[] cartasMesa, uint valor);
    }
}
