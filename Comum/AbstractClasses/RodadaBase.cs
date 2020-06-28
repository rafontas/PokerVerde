using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.AbstractClasses
{
    public abstract class RodadaBase : IRodada
    {
        public Carta[] CartasMesa { get; set; }
        public uint ValorPotePreRodada { get; set; }
        public TipoRodada TipoRodada { get; protected set; }
        private IList<IAcaoTomada> pilhaAcoes { get; set; }
        public IList<IAcaoTomada> PilhaAcoes {
            get {
                IList<IAcaoTomada> retorno = new List<IAcaoTomada>();
                foreach(IAcaoTomada acao in this.pilhaAcoes)
                    retorno.Add(acao.Clone());

                return retorno;
            }
        }
        public RodadaBase(Carta [] cartaMesa, uint valorPotePreRodada, TipoRodada tipo)
        {
            this.TipoRodada = tipo;
            this.CartasMesa = cartaMesa;
            this.ValorPotePreRodada = valorPotePreRodada;
            this.pilhaAcoes = new List<IAcaoTomada>();
        }
        public void AddAcaoTomada(IAcaoTomada acao) => this.pilhaAcoes.Add(acao);
    }
}
