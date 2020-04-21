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

        public Mesa (ConfiguracaoTHBonus RegrasMesaAtual)
        {
            this.RegrasMesaAtual = RegrasMesaAtual;
        }
        
        public ConfiguracaoTHBonus RegrasMesaAtual { get; private set; }

        public void AddParticipante(IJogador j) => this.Participantes.Add(j);

        public IDictionary<IJogador, IPartida> PartidasAtuais { get; set; } 
            = new Dictionary<IJogador, IPartida>();

        public IList<IJogador> ParticipantesJogando { get => this.PartidasAtuais.Keys.ToList<IJogador>(); }

        public IList<IJogador> Participantes { get; private set; } = new List<IJogador>();

        public void ReiniciarMesa()
        {
            this.PartidasAtuais = new Dictionary<IJogador, IPartida>();
        }
        
    }
}
