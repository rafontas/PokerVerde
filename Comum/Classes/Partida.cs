using Comum.AbstractClasses;
using Comum.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes
{
    public class Partida : PartidaBase
    {
        public Partida(uint seq) => this.SequencialPartida = seq;

        public override IPartida Clone() =>
            new Partida(this.SequencialPartida) {
                CartasMesa = this.CartasMesa,
                SequencialPartida = this.SequencialPartida,
                PoteAgora = this.PoteAgora,
                Rodadas = this.Rodadas
            };
    }
}
