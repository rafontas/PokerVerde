using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH
{
    public class DummyJogadorTHB : JogadorBase
    {
        public DummyJogadorTHB(ConfiguracaoTHBonus Config, uint valorStackInicial = 200) : base(Config, valorStackInicial)
        {
            this.config = Config;
            this.stack = valorStackInicial;
        }

        public override TipoJogador getTipoJogador() => TipoJogador.Dummy;

        public override AcaoJogador PreFlop() => new AcaoJogador(TipoAcao.Call, 0, TipoRodada.PreFlop);

        public override AcaoJogador Flop(Carta[] cartasMesas) => new AcaoJogador(TipoAcao.Call, 0, TipoRodada.Flop);

        public override AcaoJogador PosRiver(Carta[] cartasMesas) => new AcaoJogador(TipoAcao.Check, 0, TipoRodada.PosRiver);

        public override AcaoJogador PreJogo() => this.TenhoStackParaJogar() ? 
            new AcaoJogador(TipoAcao.Play, this.config.Ant, TipoRodada.PreJogo) : 
            new AcaoJogador(TipoAcao.Stop, 0, TipoRodada.PreJogo);

        public override AcaoJogador River(Carta[] cartasMesas)
        {
            throw new NotImplementedException();
        }

        public override AcaoJogador Turn(Carta[] cartasMesas)
        {
            throw new NotImplementedException();
        }
    }
}
