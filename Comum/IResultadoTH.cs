using Modelo;
using System.Collections.Generic;

namespace Modelo
{
    public interface IResultadoTH
    {
        /// <summary>
        /// Retorna a lista de jogadores que ganharam a mão.
        /// </summary>
        /// <returns></returns>
        IList<IJogador> Ganhador();
    }
}
