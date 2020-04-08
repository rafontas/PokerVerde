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
        Carta[] CartasMesa { get; set; }
        uint PoteAgora { get; }
        void AddToPote(uint valor);
        IList<IRodada> Rodadas { get; }
        void AddRodada(IRodada rodada);
        IJogador Banca { get; }
        IJogador Jogador { get; }

        VencedorPartida JogadorGanhador { get; set; }
    }
}
