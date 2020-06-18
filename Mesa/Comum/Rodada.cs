using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MesaTH.Comum
{
    public class Rodada : IRodada
    {
        public TipoRodada TipoRodada => throw new NotImplementedException();

        public Carta[] CartasMesa { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Carta[] CartasJogador { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint ValorPotePreRodada { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAcaoTomada AcaoTomada { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
