using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDAO.Interface
{
    public interface IProbabilidadeMaoInicial
    {
        uint NumCarta1 { get; set; }
        uint NumCarta2 { get; set; }
        char OffOrSuited { get; set; }
        float Probabilidade { get; set; }
        uint QuantidadesJogosSimulados { get; set; }
    }
}
