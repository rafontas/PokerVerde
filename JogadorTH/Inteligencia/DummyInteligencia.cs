using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogadorTH.Inteligencia
{
    public class DummyInteligencia : InteligenciaBase
    {
        private uint seqAcao = 1;
        private uint sequencialAcao
        {
            get { return seqAcao++; }
            set { seqAcao = value; }
        }

        private string idMente { get; set; }
        public override string IdMente { get => this.idMente; }

        private int versaoIdMente { get; set; }
        public override int VersaoIdMente { get => this.versaoIdMente; }

        public DummyInteligencia(ConfiguracaoTHBonus Config) : base()
        {
            this.sequencialAcao = 1;
            this.Config = new ConfiguracaoTHBonus();
        }

        public DummyInteligencia()
        {
            this.sequencialAcao = 1;
        }

        public override AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa)
        {
            switch (tipoRodada)
            {
                case TipoRodada.PreJogo: return this.PreJogo(valor);
                case TipoRodada.PreFlop: return this.PreFlop(valor);
                case TipoRodada.Flop: return this.Flop(cartasMesa, valor);
                case TipoRodada.Turn: return this.Turn(cartasMesa, valor);
                case TipoRodada.River: return this.River(cartasMesa);
                case TipoRodada.FimDeJogo: return this.FimDeJogo();
                default: throw new Exception("Tipo de rodada não encontrada.");
            }
        }
        
        public override AcaoJogador PreJogo(uint valor)
        {
            if (this.PossoPagarValor(this.Config.Ant + this.Config.Flop))
            {
                return (new AcaoJogador(AcoesDecisaoJogador.Play, valor, this, this.sequencialAcao));
            }
            
            return (new AcaoJogador(AcoesDecisaoJogador.Stop, 0, this, this.sequencialAcao));
        }

        public override AcaoJogador PreFlop(uint valor)
        {
            if (this.PossoPagarValor(this.Config.Ant + this.Config.Flop))
            {
                return new AcaoJogador(AcoesDecisaoJogador.PayFlop, valor, this, this.sequencialAcao);
            }

            return new AcaoJogador(AcoesDecisaoJogador.PayFlop, valor, this, this.sequencialAcao);
        }
        
        public override AcaoJogador Flop(Carta[] cartasMesa, uint valor)
        {
            if(valor == 0)
            {
                return new AcaoJogador(AcoesDecisaoJogador.Check, 0, this, this.sequencialAcao);
            }
            else if (this.PossoPagarValor(valor))
            {
                return new AcaoJogador(AcoesDecisaoJogador.Call, valor, this, this.sequencialAcao);
            }

            return new AcaoJogador(AcoesDecisaoJogador.Fold, 0, this, this.sequencialAcao);
        }

        public override AcaoJogador Turn(Carta[] cartasMesa, uint valor)
        {
            if (valor == 0)
            {
                return new AcaoJogador(AcoesDecisaoJogador.Check, 0, this, this.sequencialAcao);
            }
            else if (this.PossoPagarValor(valor))
            {
                return new AcaoJogador(AcoesDecisaoJogador.Call, valor, this, this.sequencialAcao);
            }

            return new AcaoJogador(AcoesDecisaoJogador.Fold, 0, this, this.sequencialAcao);
        }

        public override AcaoJogador River(Carta[] cartasMesa) 
            => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this, this.sequencialAcao);

        public override AcaoJogador FimDeJogo() => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this, this.sequencialAcao);
    }
}
