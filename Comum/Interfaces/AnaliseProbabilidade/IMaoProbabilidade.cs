using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface IMaoProbabilidade
    {
        Carta [] Cartas { get; }

        float ProbabilidadeVitoria { get; set; }
    }
}
