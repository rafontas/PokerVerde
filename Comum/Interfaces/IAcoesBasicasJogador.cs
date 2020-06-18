using Modelo;

namespace Comum.Interfaces
{
    public interface IAcoesBasicasJogador
    {
        uint Id { get; }

        uint Stack { get; }
        uint StackInicial { get; }

        Carta[] Cartas { get; }

        void RecebeCarta(Carta c1, Carta c2);

        void ResetaMao();

        uint RecebeValor(uint Valor);

        uint PagaValor(uint Valor);
    }
}
