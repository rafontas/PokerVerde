using Comum.Interfaces;
using Enuns;
using Modelo;

namespace JogadorTH.Inteligencia
{
    public abstract class InteligenciaBase : IAcoesDecisao
    {
        public abstract string IdMente { get; }

        public abstract int VersaoIdMente { get; }

        public IJogadorStack JogadorStack { get; set; }
        
        public string GetMinhaMao()
        {
            string minhaMao = string.Empty + "[";

            minhaMao += this.JogadorStack.Mao[0].ToString() + " ";
            minhaMao += this.JogadorStack.Mao[1].ToString();

            minhaMao += "]";

            return minhaMao;
        }

        public ICorrida Corrida { get; set; }

        public InteligenciaBase() {}

        public bool PossoPagarValor(uint ValorAhPagar) => (this.JogadorStack.Stack >= ValorAhPagar);

        public ConfiguracaoTHBonus Config { get; set; } = new ConfiguracaoTHBonus();

        public abstract AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa);

        public abstract AcaoJogador PreJogo(uint valor);

        public abstract AcaoJogador PreFlop(uint valor);

        public abstract AcaoJogador Flop(Carta[] cartasMesa, uint valor);

        public abstract AcaoJogador River(Carta[] cartasMesa);

        public abstract AcaoJogador FimDeJogo();

        public abstract AcaoJogador Turn(Carta[] cartasMesa, uint valor);

        public string JustificativaAcao(TipoRodada tipoRodada) => string.Empty;
    }
}
