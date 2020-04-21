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

        private uint seqAcao = 1;

        private uint sequencialAcao
        {
            get { return seqAcao++; }
            set { seqAcao = value; }
        }

        public ConfiguracaoTHBonus config { get; protected set; }

        public DummyInteligencia() {
            this.sequencialAcao = 1;
        }

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
        public AcaoJogador PreJogo(uint valor)
                => new AcaoJogador(AcoesDecisaoJogador.Play, valor, this, this.sequencialAcao);

        public AcaoJogador PreFlop(uint valor) 
            => new AcaoJogador(AcoesDecisaoJogador.PayFlop, valor, this, this.sequencialAcao);


        public AcaoJogador Flop(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador a = (valor == 0) ? 
                new AcaoJogador(AcoesDecisaoJogador.Check, 0, this, this.sequencialAcao) :
                new AcaoJogador(AcoesDecisaoJogador.Call, valor, this, this.sequencialAcao);

            return a;
        }

        public AcaoJogador Turn(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador a = (valor == 0) ?
                new AcaoJogador(AcoesDecisaoJogador.Check, 0, this, this.sequencialAcao) :
                new AcaoJogador(AcoesDecisaoJogador.Call, valor, this, this.sequencialAcao);

            return a;
        }

        public AcaoJogador River(Carta[] cartasMesa) 
            => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this, this.sequencialAcao);

        public AcaoJogador FimDeJogo() => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this, this.sequencialAcao);
    }
}
