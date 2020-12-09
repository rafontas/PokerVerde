using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface ISimulacaoCallPreFlop
    {
        int Id { get; set; }
        int IdGrupo { get; set; }
        uint QuantidadeJogosSimulados { get; set; }
        uint QuantidadeJogosSimuladosPretendidos { get; set; }
        uint QuantidadeJogosGanhos { get; set; }
        uint QuantidadeJogosPerdidos { get; set; }
        uint QuantidadeJogosEmpatados { get; set; }
        uint StackInicial { get; set; }
        uint StackFinal { get; set; }
        bool RaiseFlop { get; set; }
        bool RaiseFlopTurn { get; set; }
        IProbabilidadeMaoInicial ProbabilidadeMaoInicial { get; set; }
        IMaoBasica MaoBasica { get; set; }

    }
}
