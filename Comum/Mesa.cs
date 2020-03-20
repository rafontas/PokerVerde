using Comum.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comum
{
    public class Mesa
    {
        public Deck<Carta> Deck { get; set; } = new Deck<Carta>();
                
        public Carta[] Flop { get; set; } = new Carta[] { null, null, null };
        public Carta Turn = null;
        public Carta River = null;

        public List<Carta> GetCartasMesa { 
            get
            {
                List<Carta> cartasMesa = new List<Carta>();

                foreach (var c in Flop) cartasMesa.Add(c);

                if(this.Turn == null) cartasMesa.Add(Turn);
                
                if(this.River == null) cartasMesa.Add(River);

                return cartasMesa;
            } 
        }
        public Mesa (ConfiguracaoTHBonus RegrasMesaAtual)
        {
            this.RegrasMesaAtual = RegrasMesaAtual;
        }
        public ConfiguracaoTHBonus RegrasMesaAtual { get; private set; }
        public Carta[] CartasMesa { get; set; }
        public void AddParticipante(IJogador j) => this.Participantes.Add(j);

        public IDictionary<IJogador, IPartida> PartidasAtuais { get; set; } 
            = new Dictionary<IJogador, IPartida>();

        public IList<IJogador> ParticipantesJogando { get => this.PartidasAtuais.Keys.ToList<IJogador>(); }

        public IList<IJogador> Participantes { get; private set; } = new List<IJogador>();

        public void ReiniciarMesa()
        {
            this.PartidasAtuais = new Dictionary<IJogador, IPartida>();

            this.Flop = new Carta[] { null, null, null };
            this.Turn = null;
            this.River = null;

            this.Deck = new Deck<Carta>();
        }
        
    }
}
