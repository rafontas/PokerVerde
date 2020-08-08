using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IImprimePartida
    {
        string pequenoResumo (IPartida partida);

        string pequenoResumo (IList<IPartida> partidas);

        string pequenoResumoTodasPartidas (IList<IPartida> partidas);

    }
}
