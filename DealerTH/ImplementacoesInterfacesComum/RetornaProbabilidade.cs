using Comum.Interfaces.AnaliseProbabilidade;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaoTH.ImplementacoesInterfacesComum
{
    public class RetornaProbabilidade : IRetornaProbabilidade
    {
        public float GetProbabilidadeVitoria(Carta[] mao)
        {
            throw new NotImplementedException();
        }

        public float GetProbabilidadeVitoria(Carta[] mao, Carta[] flop)
        {
            throw new NotImplementedException();
        }

        public float GetProbabilidadeVitoria(Carta[] mao, Carta[] flop, Carta turn)
        {
            throw new NotImplementedException();
        }

        public float GetProbabilidadeVitoria(Carta[] mao, Carta[] flop, Carta turn, Carta river)
        {
            throw new NotImplementedException();
        }
    }
}
