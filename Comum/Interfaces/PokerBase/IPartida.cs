using System;
using System.Collections.Generic;
using System.Text;
using Enuns;
using Modelo;

namespace Comum.Interfaces
{
    public interface IPartida : IClone<IPartida>
    {
        uint SequencialPartida { get; }
        Carta[] CartasMesa { get; }
        uint PoteAgora { get; }
        uint ValorInvestidoBanca { get; }
        uint ValorInvestidoJogador { get; }
        void AddToPote(uint valor, TipoJogadorTHB tipoJogador);
        IList<IRodada> Rodadas { get; }
        void AddRodada(IRodada rodada);
        void RevelarFlop();
        void RevelarTurn();
        void RevelarRiver();
        Carta PopDeck();

        IJogador Banca { get; }
        IJogador Jogador { get; }

        VencedorPartida JogadorGanhador { get; set; }
    }
}
