using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH.Acoes
{
    public class DummyInteligencia : IAcoesDecisao
    {
        public string idMente { get; protected set; }
        public int versaoIdMente { get; protected set; }
        public ConfiguracaoTHBonus config { get; protected set; }

        public AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa)
        {
            switch (tipoRodada)
            {
                case TipoRodada.PreJogo: return this.PreJogo(valor);
                case TipoRodada.PreFlop: return this.PreFlop(valor);
                case TipoRodada.Flop: return this.Flop(cartasMesa, valor);
                case TipoRodada.Turn: return this.Turn(cartasMesa, valor);
                case TipoRodada.River: return this.River(cartasMesa);
                case TipoRodada.FimDeJogo: return this.FimDeJogo();
                default: throw new Exception("Tipo de rodada não encontrada.");
            }
        }

        public AcaoJogador FimDeJogo() => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);

        public AcaoJogador Flop(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador a = (valor == 0) ? new AcaoJogador(AcoesDecisaoJogador.Check, 0, this) :
                         new AcaoJogador(AcoesDecisaoJogador.Call, valor, this);

            return a;
        }

        public AcaoJogador PreFlop(uint valor) => new AcaoJogador(AcoesDecisaoJogador.Check, valor, this);

        public AcaoJogador PreJogo(uint valor) => new AcaoJogador(AcoesDecisaoJogador.Play, valor, this);

        public AcaoJogador River(Carta[] cartasMesa) => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);

        public AcaoJogador Turn(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador a = (valor == 0) ?
                new AcaoJogador(AcoesDecisaoJogador.Check, 0, this) :
                new AcaoJogador(AcoesDecisaoJogador.Call, valor, this);

            return a;
        }
    }
}
