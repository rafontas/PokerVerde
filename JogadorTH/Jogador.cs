using Enuns;
using Modelo;
using System;

namespace JogadorTH
{
    public class Jogador : IJogador
    {
        private Carta[] cartas = new Carta[] { null, null };
        private MomentoJogo momento = MomentoJogo.PreJogo;
        private uint Stack { get; set; }

        public uint GetStack() => Stack;

        public uint PagaValor(uint Valor) => (Stack -= Valor);

        public Carta[] GetCartas
        {
            get => cartas;
            set => cartas = value;
        }

        public MomentoJogo Momento {
            get => momento;
            private set => momento = value;
        }

        public void RecebeCarta(Carta c1, Carta c2)
        {
            cartas[0] = c1;
            cartas[1] = c2;
        }

        public uint RecebeValor(uint Valor) => (Stack += Valor);

        public void ResetaMao()
        {
            cartas = new Carta[] { null, null };
            momento = MomentoJogo.PreJogo;
        }

        public Jogador(uint valorStackInicial = 200)
        {
            Stack = valorStackInicial;
        }

        /// <summary>
        /// Executa a ação do jogador dado o momento do jogo;
        /// </summary>
        /// <param name="momento">O momento do jogo.</param>
        /// <param name="valoPagar">Valor a pagar.</param>
        /// <returns>Retorna a ação e o valor pago.</returns>
        public AcaoJogador ExecutaAcao(MomentoJogo momento, uint valoPagar)
        {
            AcaoJogador acao = null;
            switch (momento)
            {
                case MomentoJogo.PreJogo: acao = PreJogo(valoPagar); break;
                case MomentoJogo.PreFlop: acao = PreFlop(valoPagar); break;
                case MomentoJogo.Flop: acao = Flop(valoPagar); break;
                case MomentoJogo.Turn: acao = Turn(valoPagar); break;
                case MomentoJogo.River: acao = River(valoPagar); break;
                case MomentoJogo.PosRiver: acao = PosRiver(valoPagar); break;
                case MomentoJogo.FimDeJogo: break;
                default: throw new Exception("Momento de jogo não especificado.");
            }

            return acao;
        }

        private AcaoJogador PreJogo(uint valor)
            => new AcaoJogador(TipoAcao.Play, 0, MomentoJogo.PreJogo);

        private AcaoJogador PreFlop(uint valor)
            => new AcaoJogador(TipoAcao.Call, 0, MomentoJogo.PreFlop);

        private AcaoJogador Flop(uint valor)
            => new AcaoJogador(TipoAcao.Call, 0, MomentoJogo.Flop);

        private AcaoJogador Turn(uint valor)
            => new AcaoJogador(TipoAcao.Check, 0, MomentoJogo.Turn);

        private AcaoJogador River(uint valor)
            => new AcaoJogador(TipoAcao.Check, 0, MomentoJogo.River);

        private AcaoJogador PosRiver(uint valor)
            => new AcaoJogador(TipoAcao.Check, 0, MomentoJogo.PosRiver);

        public void AvancaMomento()
        {
            if(Momento != MomentoJogo.FimDeJogo) Momento++;
        }
    }
}
