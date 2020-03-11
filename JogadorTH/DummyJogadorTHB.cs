using Comum.Excecoes;
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

        public override AcaoJogador PreJogo(uint valor) => this.TenhoStackParaJogar() ?
            new AcaoJogador(TipoAcao.Play, this.config.Ant, TipoRodada.PreJogo) :
            new AcaoJogador(TipoAcao.Stop, 0, TipoRodada.PreJogo);
        public override AcaoJogador PreFlop(uint valor) => new AcaoJogador(TipoAcao.Call, 0, TipoRodada.PreFlop);
        public override AcaoJogador Flop(Carta[] cartasMesas, uint valor) => new AcaoJogador(TipoAcao.Call, 0, TipoRodada.Flop);
        public override AcaoJogador Turn(Carta[] cartasMesas, uint valor) => new AcaoJogador(TipoAcao.Call, 0, TipoRodada.Turn);
        public override AcaoJogador River(Carta[] cartasMesas) => new AcaoJogador(TipoAcao.Call, 0, TipoRodada.River);
        public override AcaoJogador FimDeJogo() => new AcaoJogador(TipoAcao.Check, 0, TipoRodada.PosRiver);
    }
}
