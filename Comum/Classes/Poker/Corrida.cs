using Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comum.Classes
{
    public class Corrida : ICorrida
    {
        private uint QtdPartidasAhSimular { get; set; }

        public uint QtdPartidasJogadas { 
            get => (uint) this.ListaPartidas.Count; 
        }

        public uint ValorAtual { get; set; } = 0;

        public uint ValorGanho { get; set; } = 0;

        public uint ValorPerdido { get; set; } = 0;

        public uint QtdVitoriasJogador { 
            get => (uint) this.ListaPartidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count(); 
        }

        public uint QtdEmpates { 
            get => (uint) this.ListaPartidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Empate).Count(); 
        }

        public uint QtdDerrotasJogador { 
            get => (uint)this.ListaPartidas.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Banca).Count(); 
        }

        public IList<IPartida> ListaPartidas { get; set; }

        public Corrida(uint QtdPartidasASimular = 0) { 
            this.QtdPartidasAhSimular = QtdPartidasASimular;
            this.ListaPartidas = new List<IPartida>();
        }

        public bool HaPartidaParaJogar() 
            => (this.QtdPartidasAhSimular == 0 ? false : (this.QtdPartidasAhSimular > this.QtdPartidasJogadas));

        public void AddPartida(IPartida p)
        {
            // Adiciona partida ao histórico
            this.ListaPartidas.Add(p);
            this.ValorAtual = p.Jogador.Stack;

            switch (p.JogadorGanhador)
            {
                case Enuns.VencedorPartida.Banca:
                    this.ValorPerdido += p.ValorInvestidoJogador;
                    break;

                case Enuns.VencedorPartida.Jogador:
                    this.ValorGanho += p.ValorInvestidoBanca;
                    break;

                case Enuns.VencedorPartida.Empate:
                    break;

                default:
                    throw new Exception("Vencedor de partida não encontrado.k");
            }
        }
    }
}
