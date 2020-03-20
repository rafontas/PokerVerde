using System;
using System.Collections.Generic;
using System.Text;
using Modelo;
using Enuns;

namespace Comum.Interfaces
{
    public interface IAcaoTomada
    {
        uint Sequencial { get; }
        IAcoesDecisao AcaoDecisao { get; }
        uint ValorAcaoTomada { get; }
        AcoesDecisaoJogador Acao { get; }
    }
}
