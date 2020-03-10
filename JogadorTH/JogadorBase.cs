using Enuns;
using Modelo;
using System;

namespace JogadorTH
{
    public abstract class JogadorBase : IJogador, IJogadorAcoesTHBonus
    {
        public abstract TipoJogador getTipoJogador();
        public uint id { get; set; }

        public uint Id { get => this.id;  }

        protected uint stack { get; set; }

        public uint Stack { get => this.stack; }

        public uint PagaValor(uint Valor) => (this.stack -= Valor);

        public Carta[] cartas { get; set; } = new Carta[] { null, null };
        public Carta[] Cartas { get => this.cartas; } 

        public TipoRodada Momento { get; private set; } = TipoRodada.PreJogo;
        public ConfiguracaoTHBonus config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void RecebeCarta(Carta c1, Carta c2)
        {
            this.cartas[0] = c1;
            this.cartas[1] = c2;
        }

        public uint RecebeValor(uint Valor) => this.stack += Valor;

        public void ResetaMao()
        {
            this.cartas = new Carta[] { null, null };
            Momento = TipoRodada.PreJogo;
        }

        public JogadorBase(ConfiguracaoTHBonus Config, uint valorStackInicial = 200)
        {
            this.config = Config;
            this.stack = valorStackInicial;
        }

        /// <summary>
        /// Executa a ação do jogador dado o momento do jogo;
        /// </summary>
        /// <param name="momento">O momento do jogo.</param>
        /// <param name="valoPagar">Valor a pagar.</param>
        /// <returns>Retorna a ação e o valor pago.</returns>
        public AcaoJogador ExecutaAcao(TipoRodada momento, uint valoPagar, Carta[] mesa)
        {
            AcaoJogador acao = null;
            switch (momento)
            {
                case TipoRodada.PreJogo: acao = PreJogo(valoPagar); break;
                case TipoRodada.PreFlop: acao = PreFlop(valoPagar); break;
                case TipoRodada.Flop: acao = Flop(valoPagar); break;
                case TipoRodada.Turn: acao = Turn(valoPagar); break;
                case TipoRodada.River: acao = River(valoPagar); break;
                case TipoRodada.PosRiver: acao = PosRiver(valoPagar); break;
                case TipoRodada.FimDeJogo: break;
                default: throw new Exception("Momento de jogo não especificado.");
            }

            return acao;
        }
        public void AvancaMomento()
        {
            if(Momento != TipoRodada.FimDeJogo) Momento++;
        }

        public abstract AcaoJogador PreJogo();

        public abstract AcaoJogador PreFlop();

        public abstract AcaoJogador Flop(Carta[] cartasMesas);

        public abstract AcaoJogador Turn(Carta[] cartasMesas);

        public abstract AcaoJogador River(Carta[] cartasMesas);

        public abstract AcaoJogador PosRiver(Carta[] cartasMesas);

        public bool TenhoStackParaJogar() =>
            this.Stack >= (this.config.Ant + this.config.Flop);
    }
}
