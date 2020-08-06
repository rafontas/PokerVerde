using Comum.Interfaces;
using DealerTH.Probabilidade;
using Enuns;
using JogadorTH.Inteligencia.Probabilidade;
using Modelo;
using System;

namespace JogadorTH.Inteligencia
{
    public class InteligenciaProb : InteligenciaBase
    {
        private string idMente { get; set; }
        public override string IdMente { get => this.idMente; }
        private int versaoIdMente { get; set; }
        public override int VersaoIdMente { get => this.versaoIdMente; }

        private AcaoProbabilidade AcaoProbabilidade { get; set; }

        public InteligenciaProb() : base()
        {
            this.AcaoProbabilidade = new AcaoProbabilidade();
        }

        public override AcaoJogador ExecutaAcao(TipoRodada tipoRodada, uint valor, Carta[] cartasMesa)
        {
            switch (tipoRodada) {
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
            AcoesDecisaoJogador acao;
            bool HaJogosParaJogar = this.Corrida?.HaPartidaParaJogar() ?? true;
            
            if (this.PossoPagarValor(this.Config.Ant + this.Config.Flop) || HaJogosParaJogar)
            {
                acao = AcoesDecisaoJogador.Play;
            }
            else
            {
                acao = AcoesDecisaoJogador.Stop;
            }

            return new AcaoJogador(acao, 0, this);
        }

        public override AcaoJogador PreFlop(uint valor) 
        {
            AcaoJogador acao;
            float minhaProbAgora = this.getMinhaProbalidadeAgora(this.JogadorStack.Mao);

            if (minhaProbAgora >= this.AcaoProbabilidade.probMinimaChamarFlop)
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.PayFlop, this.Config.Flop, this);
            }
            else
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Fold, 0, this);
            }

            return acao;
        }

        public override AcaoJogador Flop(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador acao;

            if (this.getMinhaProbalidadeAgora(this.JogadorStack.Mao) >= this.AcaoProbabilidade.probMinAumentaNoFlop)
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Raise, this.Config.Turn, this);
            }
            else
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);
            }

            return acao;
        }
        
        public override AcaoJogador Turn(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador acao;

            if (this.getMinhaProbalidadeAgora(this.JogadorStack.Mao) >= this.AcaoProbabilidade.probMinAumentaNoTurn)
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Raise, this.Config.River, this);
            }
            else
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);
            }

            return acao;
        }

        public override AcaoJogador River(Carta[] cartasMesa) => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);

        public override AcaoJogador FimDeJogo() => new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);

        /// <summary>
        /// Calcula a probabilidade de ganhar
        /// </summary>
        /// <param name="minhasCartas">cartas na mão</param>
        /// <param name="cartasMesa">cartas na mesa</param>
        /// <returns></returns>
        private float getMinhaProbalidadeAgora(Carta[] minhasCartas, Carta[] cartasMesa = null) 
        {
            return (cartasMesa == null ?
                AvaliaProbabilidadeMao.GetPorcentagemVitoria(minhasCartas) :
                AvaliaProbabilidadeMao.GetPorcentagemVitoria(minhasCartas, cartasMesa));
        }
    }
}
