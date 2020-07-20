using Comum.Classes;
using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testes.Modelos.Mocks
{
    internal class DealerPartidaMock : DealerPartida
    {
        public DealerPartidaMock(Comum.Mesa m, IJogador banca, VencedorPartida jogadorQueDeveGanharPartida) : base(m, banca)
        {
            this.JogadorQueDeveGanharPartida = jogadorQueDeveGanharPartida;
        }

        public VencedorPartida JogadorQueDeveGanharPartida { get; set; }

        /// <summary>
        /// Permite atribuir a vitória a um jogador arbitrariamente. Para testes.
        /// </summary>
        /// <param name="p">Partida</param>
        /// <param name="jogadorQueDeveGanharPartida">Jogador</param>
        public override void VerificarGanhadorPartida(IPartida p) 
        {
            p.JogadorGanhador = this.JogadorQueDeveGanharPartida;
        }

    }
}
