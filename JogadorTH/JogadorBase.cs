using Comum.Classes;
using Comum.Excecoes;
using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JogadorTH
{
    public abstract class JogadorBase : IJogador
    {
        private static uint _idCount = 0;
        private static uint getNovoId { get => JogadorBase._idCount++; }
        private uint id { get; set; }
        public uint Id => this.id;
        public uint StackInicial { get => this.JogadorStack.StackInicial; }
        public uint Stack { get => this.JogadorStack.Stack; }
        public IJogadorStack JogadorStack { get; private set; }

        public uint PagarValor(uint ValorAhPagar) => this.JogadorStack.PagarValor(ValorAhPagar);

        public void ReceberValor(uint ValorAhReceber) => this.JogadorStack.ReceberValor(ValorAhReceber);
        
        public Carta[] Cartas { get => this.JogadorStack.Mao; } 

        protected ConfiguracaoTHBonus config { get; set; }

        public IList<IAcoesDecisao> Mente { get; set; } = new List<IAcoesDecisao>();

        public void ReceberCarta(Carta c1, Carta c2) => this.JogadorStack.ReceberCarta(c1, c2);

        public void ResetaMao() => this.JogadorStack.ResetaMao();

        public ICorrida Corrida { get; set; } = new Corrida();

        private IList<IPartida> historico { get; set; } = new List<IPartida>();

        public IList<IPartida> Historico { get => new List<IPartida>(this.historico); }

        private uint seqProximaPartida { get; set; }

        public uint SeqProximaPartida { get => this.seqProximaPartida++; }

        public IJogadorEstatisticas Estatistica { get; }

        public IList<IImprimePartida> ImprimePartida { get; protected set; }

        public void AddPartidaHistorico(IPartida p) 
        {
            this.historico.Add(p);
            this?.Corrida.ListaPartidas.Add(p);
        }

        public JogadorBase(ConfiguracaoTHBonus Config, uint valorStackInicial = 200)
        {
            this.config = Config;
            this.Estatistica = new JogadorEstatisticas(this.historico);
            this.id = JogadorBase.getNovoId;
            this.seqProximaPartida = 0;
            this.JogadorStack = new JogadorStack(valorStackInicial);
            this.ImprimePartida = new List<IImprimePartida>(){ new ImprimePartida() };
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
        
        public bool VouJogarMaisUmaPartida()
        {
            bool haPartidaParaJogar = this.Corrida?.HaPartidaParaJogar() ?? false;

            if (!this.TenhoStackParaJogar() || haPartidaParaJogar) 
                return false;

            return true;
        }

        public int GetHashCode(IJogador obj) => obj.GetHashCode();

        public bool Equals(IJogador x, IJogador y) => x.Id == y.Id;

        public AcaoJogador PreJogo(uint valor) 
        {
            if (!this.VouJogarMaisUmaPartida()) 
                return new AcaoJogador(AcoesDecisaoJogador.Stop, 0, null, 0);

            return this.Mente.First().PreJogo(valor);
        }

        public AcaoJogador PreFlop(uint valor)
        {
            if (!this.TenhoStackParaJogar()) 
                return new AcaoJogador(AcoesDecisaoJogador.Stop, 0, null, 0);
         
            return this.Mente.First().PreFlop(valor); 
        }

        public AcaoJogador Flop(Carta[] cartasMesa, uint valor) 
        { 
            return this.Mente.First().Flop(cartasMesa, valor);
        }

        public AcaoJogador Turn(Carta[] cartasMesa, uint valor)
        {
            return this.Mente.First().Turn(cartasMesa, valor);
        }

        public AcaoJogador River(Carta[] cartasMesa)
        {
            return this.Mente.First().River(cartasMesa);
        }

        public AcaoJogador FimDeJogo()
        {
            return this.Mente.First().FimDeJogo();
        }
    }
}
