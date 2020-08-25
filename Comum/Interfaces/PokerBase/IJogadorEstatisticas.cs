using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IJogadorEstatisticas
    {
        int getQuantidadeJogosJogados();
        int getStackInicial();
        int getStackFinal();
        int getStackSaldoFinal();
        int getQuantidadeJogosGanhos();
        int getQuantidadeJogosPerdidos();
        float getValorGanhoPorJogo();
    }
}
