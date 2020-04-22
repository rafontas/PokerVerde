using Comum.Excecoes;
using Comum.Interfaces;
using Enuns;
using JogadorTH.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogadorTH
{
    public abstract class JogadorBase : IJogador
    {
        public uint id { get; set; }

        public uint Id { get => this.id;  }

        protected uint stack { get; set; }

        public uint Stack { get => this.stack; }

        public uint PagaValor(uint Valor) {
            if (this.stack < Valor) throw new JogadorException("Não há stack para pagar o valor passado.");
            this.stack -= Valor;
            return Valor;
        }

        public Carta[] cartas { get; set; } = new Carta[] { null, null };
        
        public Carta[] Cartas { get => this.cartas; } 

        protected ConfiguracaoTHBonus config { get; set; }

        public IList<IAcoesDecisao> Mente { get; set; } = new List<IAcoesDecisao>();

        public void RecebeCarta(Carta c1, Carta c2)
        {
            this.cartas[0] = c1;
            this.cartas[1] = c2;
        }

        public uint RecebeValor(uint Valor) {
            this.stack += Valor;
            return Valor;
        } 

        public void ResetaMao() => this.cartas = new Carta[] { null, null };

        public ICorrida Corrida { get; set; } = new Corrida();

        private IList<IPartida> historico { get; set; } = new List<IPartida>();

        public IList<IPartida> Historico {
            get => new List<IPartida>(this.historico);
        }

        private uint seqProximaPartida { get; set; } = 0;

        public uint SeqProximaPartida { get => this.seqProximaPartida; set => this.seqProximaPartida++; }

        public void AddPartidaHistorico(IPartida p) 
        {
            this.historico.Add(p); 
            SeqProximaPartida++;
        }

        public JogadorBase(ConfiguracaoTHBonus Config, uint valorStackInicial = 200)
        {
            this.config = Config;
            this.stack = valorStackInicial;
        }

        public AcaoJogador ExecutaAcao(TipoRodada momento, uint valoPagar, Carta[] mesa)
        {
            AcaoJogador acao = null;
            switch (momento)
            {
                case TipoRodada.PreJogo: acao = this.Mente.First().PreJogo(valoPagar); break;
                case TipoRodada.PreFlop: acao = this.Mente.First().PreFlop(valoPagar); break;
                case TipoRodada.Flop: acao = this.Mente.First().Flop(mesa, valoPagar); break;
                case TipoRodada.Turn: acao = this.Mente.First().Turn(mesa, valoPagar); break;
                case TipoRodada.River: acao = this.Mente.First().River(mesa); break;
                case TipoRodada.FimDeJogo: break;
                default: throw new Exception("Momento de jogo não especificado.");
            }

            return acao;
        }
        
        public bool TenhoStackParaJogar() => this.Stack >= (this.config.Ant + this.config.Flop);

        public int GetHashCode(IJogador obj) => obj.GetHashCode();

        public bool Equals(IJogador x, IJogador y) => x.Id == y.Id;

        public AcaoJogador PreJogo(uint valor) 
        {
            if (!this.TenhoStackParaJogar()) 
                return new AcaoJogador(AcoesDecisaoJogador.Stop, 0, null, 0);

            return this.Mente.First().PreJogo(valor);
        }

        public AcaoJogador PreFlop(uint valor)
        {
            if (!this.TenhoStackParaJogar()) 
                return new AcaoJogador(AcoesDecisaoJogador.Stop, 0, null, 0);
            
            return this.Mente.First().PreFlop(valor); 
        }

        public AcaoJogador Flop(Carta[] cartasMesa, uint valor) => this.Mente.First().Flop(cartasMesa, valor);

        public AcaoJogador Turn(Carta[] cartasMesa, uint valor) => this.Mente.First().Turn(cartasMesa, valor);

        public AcaoJogador River(Carta[] cartasMesa) => this.Mente.First().River(cartasMesa);

        public AcaoJogador FimDeJogo() =>  this.Mente.First().FimDeJogo();
    }
}
