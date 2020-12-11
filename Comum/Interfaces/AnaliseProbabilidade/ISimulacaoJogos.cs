using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface ISimulacaoJogosResumo
    {
        int Id { get; set; }
        uint QuantidadeJogosSimulados { get; set; }
        uint QuantidadeJogosSimuladosPretendidos { get; set; }
        uint QuantidadeJogosGanhos { get; set; }
        uint QuantidadeJogosPerdidos { get; set; }
        uint QuantidadeJogosEmpatados { get; set; }
        uint StackInicial { get; set; }
        uint StackFinal { get; set; }
        string DescricaoInteligencia { get; set; }
        IAcaoProbabilidade AcaoProbabilidade { get; set; }
    }
}
