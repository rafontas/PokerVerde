using Comum.Interfaces;
using Enuns;
using Modelo;

namespace JogadorTH.Inteligencia
{
    public abstract class InteligenciaBase : IAcoesDecisao
    {
        private uint StackInicial { get; set; }
        private uint Stack { get; set; }

        public abstract string IdMente { get; }

        public abstract int VersaoIdMente { get; }

        public void SetStackAgora(uint StackInicial, uint Stack)
        {
            this.StackInicial = StackInicial;
            this.Stack = Stack;
        }
        
        public bool PossoPagarValor(uint ValorAhPagar) => (this.Stack >= ValorAhPagar);

        public ConfiguracaoTHBonus Config { get; set; } = new ConfiguracaoTHBonus();

        public abstract AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa);

        public abstract AcaoJogador PreJogo(uint valor);

        public abstract AcaoJogador PreFlop(uint valor);

        public abstract AcaoJogador Flop(Carta[] cartasMesa, uint valor);

        public abstract AcaoJogador River(Carta[] cartasMesa);

        public abstract AcaoJogador FimDeJogo();

        public abstract AcaoJogador Turn(Carta[] cartasMesa, uint valor);
    }
}
