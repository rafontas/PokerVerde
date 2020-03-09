using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public interface IJogadorEstatisticas
    {
        uint getQuantJogosJogados();
        uint getStackInicial();
        uint getStackAgora();
        uint getGanho();
        uint getPerdas();
        uint getGanhoPorJogo();

    }
}
