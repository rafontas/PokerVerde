
using Enuns;

namespace Modelo
{
    public interface IJogador
    {
        uint GetStack();

        void RecebeCarta(Carta c1, Carta c2);

        Carta [] GetCartas { get; set; }

        void ResetaMao();

        uint PagaValor(uint Valor);

        uint RecebeValor(uint Valor);

        void AvancaMomento();

        /// <summary>
        /// Indica o momento que o jogador está. A ultima ação que foi realizada.
        /// </summary>
        MomentoJogo Momento { get; }

        AcaoJogador ExecutaAcao(MomentoJogo momento, uint valorPagar);
    }
}
