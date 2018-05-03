using System.Collections.Generic;
using Enuns;

namespace Modelo
{
    public interface IJogo
    {
        Jogo Identifique();
        IList<Carta> Kickers();
        uint Valor();
    }
}
