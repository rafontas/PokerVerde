using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comum.AbstractClasses
{
    public abstract class PartidaBase : IPartida
    {
        public uint SequencialPartida { get; protected set; }

        public abstract Carta[] CartasMesa { get; }

        public uint PoteAgora { get; protected set; }

        public IList<IRodada> Rodadas { get; protected set; } = new List<IRodada>();

        public IJogador Banca { get; set; }

        public IJogador Jogador { get; set; }

        public VencedorPartida JogadorGanhador { get; set; }

        public void AddRodada(IRodada rodada) => this.Rodadas.Add(rodada);

        public void AddToPote(uint valor) => this.PoteAgora += valor;

        public abstract IPartida Clone();

        public abstract Carta PopDeck();

        public abstract void RevelarFlop();

        public abstract void RevelarRiver();

        public abstract void RevelarTurn();
    }
}
