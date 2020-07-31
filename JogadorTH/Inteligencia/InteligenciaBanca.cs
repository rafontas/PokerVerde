using Comum.Interfaces;
using Enuns;
using Modelo;
using System;

namespace JogadorTH.Inteligencia
{
    public class InteligenciaBanca : InteligenciaBase
    {
        private string idMente { get; set; }
        public override string IdMente { get => this.idMente; }

        private int versaoIdMente { get; set; }
        public override int VersaoIdMente { get => this.versaoIdMente; }

        public InteligenciaBanca() : base() {}

        public override AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa)
        {
            switch (tipoRodada) {
                case TipoRodada.PreJogo: return this.PreJogo(valor);
                case TipoRodada.PreFlop: return this.PreFlop(valor);
                case TipoRodada.Flop: return this.Flop(cartasMesa, valor);
                case TipoRodada.Turn: return this.Turn(cartasMesa, valor);
                case TipoRodada.River: return this.River(cartasMesa);
                case TipoRodada.FimDeJogo: return this.FimDeJogo();
                default: throw new Exception("Tipo de rodada não encontrada.");
            }
        }

        public override AcaoJogador FimDeJogo() => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);

        public override AcaoJogador Flop(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador a = (valor == 0) ? new AcaoJogador(AcoesDecisaoJogador.Check, 0, this) :
                         new AcaoJogador(AcoesDecisaoJogador.Call, valor, this);

            return a;
        }

        public override AcaoJogador PreFlop(uint valor) => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);

        public override AcaoJogador PreJogo(uint valor) => new AcaoJogador(AcoesDecisaoJogador.Play, 0, this);

        public override AcaoJogador River(Carta[] cartasMesa) => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);

        public override AcaoJogador Turn(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador a = (valor == 0) ? 
                new AcaoJogador(AcoesDecisaoJogador.Check, 0, this) :
                new AcaoJogador(AcoesDecisaoJogador.Call, valor, this);

            return a;
        }
    }
}
