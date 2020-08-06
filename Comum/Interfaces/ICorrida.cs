using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface ICorrida
    {
        void AddPartida(IPartida p);
        uint QtdPartidasJogadas { get; }
        uint ValorAtual { get; }

        uint ValorGanho { get; }
        uint ValorPerdido { get; }

        uint QtdVitoriasJogador { get; }
        uint QtdEmpates { get; }
        uint QtdDerrotasJogador { get; }

        bool HaPartidaParaJogar();

        IList<IPartida> ListaPartidas { get; set; }
    }
}
