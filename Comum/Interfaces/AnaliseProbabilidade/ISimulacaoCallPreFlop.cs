using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface ISimulacaoCallPreFlop
    {
        int Id { get; set; }
        uint QuantidadesJogosSimulados { get; set; }
        uint StackInicial { get; set; }
        uint StackFinal { get; set; }
        IProbabilidadeMaoInicial ProbabilidadeMaoInicial { get; set; }
    }
}
