using Modelo;

namespace Comum.Interfaces
{
    public interface IAcoesBasicasJogador
    {
        uint Id { get; }

        uint Stack { get; }
        uint StackInicial { get; }

        Carta[] Cartas { get; }

        void ReceberCarta(Carta c1, Carta c2);

        void ResetaMao();

        void ReceberValor(uint Valor);

        uint PagarValor(uint Valor);
    }
}
