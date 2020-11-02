using Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IProbabilidadeMaoInicial : IEquatable<IProbabilidadeMaoInicial>, IEqualityComparer<IProbabilidadeMaoInicial>
    {
        int Id { get; }
        uint NumCarta1 { get; set; }
        uint NumCarta2 { get; set; }
        char OffOrSuited { get; set; }
        float ProbabilidadeVitoria { get; set; }
        float ProbabilidadeSair { get; set; }
        uint QuantidadesJogosSimulados { get; set; }
    }
}
