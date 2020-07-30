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
        public ConfiguracaoTHBonus RegrasMesaAtual { get; private set; }

        public Mesa (ConfiguracaoTHBonus RegrasMesaAtual)
        {
            this.RegrasMesaAtual = RegrasMesaAtual;
        }

        public void AddParticipante(IJogador j) => this.JogadoresNaMesa.Add(j);

        public IDictionary<IJogador, IPartida> PartidasAtuais { get; set; } = new Dictionary<IJogador, IPartida>();

        public IList<IJogador> JogadoresNaMesa { get; private set; } = new List<IJogador>();

        public void ReiniciarMesa()
        {
            this.PartidasAtuais = new Dictionary<IJogador, IPartida>();
        }
        
    }
}
