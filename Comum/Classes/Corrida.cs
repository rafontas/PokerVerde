using Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comum.Classes
{
    public class Corrida : ICorrida
    {
        private uint QtdPartidasASimular { get; set; }

        public uint QtdPartidasJogadas { get => (uint) this.ListaPartidas.Count; }

        public uint ValorInicial { get; set; }

        public uint ValorAtual { get; set; }

        public uint ValorGanho { get; set; }

        public uint ValorPerdido { get; set; }
        
        public uint QtdVitoriasJogador { get => (uint) this.ListaPartidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count(); }

        public uint QtdEmpates { get => (uint) this.ListaPartidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Empate).Count(); }

        public uint QtdDerrotasJogador { get => (uint)this.ListaPartidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Banca).Count(); }

        public IList<IPartida> ListaPartidas { get; set; }

        public Corrida(uint QtdPartidasASimular = 0) { 
            this.QtdPartidasASimular = QtdPartidasASimular;
            this.ListaPartidas = new List<IPartida>();
        }

        public bool HaPartidaParaJogar() => (this.QtdPartidasASimular == 0 ? true : (this.QtdPartidasASimular < this.QtdPartidasJogadas));
    }
}
