using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface IRetornaProbabilidade
    {
        float GetProbabilidadeVitoria(Carta [] mao);
        float GetProbabilidadeVitoria(Carta [] mao, Carta [] mesa);
    }
}
