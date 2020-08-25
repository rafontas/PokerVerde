using System;
using System.Collections.Generic;
using System.Text;
using Modelo;
using Enuns;

namespace Comum.Interfaces
{
    public interface IAcaoTomada : IClone<IAcaoTomada>
    {
        uint Sequencial { get; set; }
        uint ValorRequerido { get; }
        uint ValorDaAcaoTomada { get; }
        IAcoesDecisao AcaoDecisao { get; }
        AcoesDecisaoJogador Acao { get; }
    }
}
