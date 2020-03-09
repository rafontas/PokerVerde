
using Enuns;

namespace Modelo
{
    public interface IJogador
    {
        uint Id { get; }

        uint Stack { get; }

        Carta [] Cartas { get; }

        void RecebeCarta(Carta c1, Carta c2);

        void ResetaMao();

        uint RecebeValor(uint Valor);

        uint PagaValor(uint Valor);

        void AvancaMomento();

        /// <summary>
        /// Indica o momento que o jogador está. A ultima ação que foi realizada.
        /// </summary>
        TipoRodada Momento { get; }

        AcaoJogador ExecutaAcao(TipoRodada momento, uint valorPagar, Carta[] mesa);
    }

    /// <summary>
    /// Representa o tipo do jogador.
    /// </summary>
    public enum TipoJogador { 
        bancaTHBonus = 1,
        Dummy = 2,
    }
}
