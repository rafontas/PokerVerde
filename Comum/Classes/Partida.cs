using Comum.AbstractClasses;
using Comum.Interfaces;
using Modelo;
using System.Collections.Generic;

namespace Comum.Classes
{
    public class Partida : PartidaBase
    {
        public Deck<Carta> Deck { get; set; } = new Deck<Carta>();

        public Carta[] Flop { get; private set; } = new Carta[] { null, null, null };
        public Carta Turn { get; private set; } = null;
        public Carta River { get; private set; } = null;

        public void SetMesa(Carta[] Flop, Carta Turn, Carta River)
        {
            this.Flop[0] = Flop[0].Clone();
            this.Flop[1] = Flop[1].Clone();
            this.Flop[2] = Flop[2].Clone();
            this.Turn = Turn.Clone();
            this.River = River.Clone();
        }

        public override void RevelarFlop()
        {
            this.Flop[0] = this.Deck.Pop();
            this.Flop[1] = this.Deck.Pop();
            this.Flop[2] = this.Deck.Pop();
        }

        public override void RevelarTurn() => this.Turn = this.Deck.Pop();

        public override void RevelarRiver() => this.River = this.Deck.Pop();

        public override Carta PopDeck() => this.Deck.Pop();

        public List<Carta> GetCartasMesa
        {
            get
            {
                List<Carta> cartasMesa = new List<Carta>();

                foreach (var c in Flop) cartasMesa.Add(c);

                if (this.Turn == null) cartasMesa.Add(Turn);

                if (this.River == null) cartasMesa.Add(River);

                return cartasMesa;
            }
        }

        public override Carta[] CartasMesa
        {
            get => new Carta[] { Flop[0], Flop[1], Flop[2], Turn, River };
        }

        public Partida(uint seq, IJogador jogador, IJogador banca)
        {
            SequencialPartida = seq;
            this.AddRodada(new RodadaTHB(Enuns.TipoRodada.PreJogo, 0, null));
            this.Jogador = jogador;
            this.Banca = banca;
            this.PoteAgora = 0;
            this.Deck.CriaDeckPadrao();
        }

        public override IPartida Clone()
        {
            Partida p = new Partida(this.SequencialPartida, this.Jogador, this.Banca)
            {
                SequencialPartida = this.SequencialPartida,
                PoteAgora = this.PoteAgora,
                Rodadas = this.Rodadas
            };

            p.SetMesa(this.Flop, this.Turn, this.River);

            return p;
        }
    }
}
