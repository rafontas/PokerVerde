using Comum.Interfaces.AnaliseProbabilidade;
using Enuns;
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
        
        public IRetornaProbabilidade RetornaProbabilidade { get; set; }

        internal IAcaoProbabilidade AcaoProbabilidade { get; set; }

        public InteligenciaProb(IAcaoProbabilidade AcaoProbabilidade, IRetornaProbabilidade RetornaProbabilidade) : base()
        {
            this.RetornaProbabilidade = RetornaProbabilidade;
            this.AcaoProbabilidade = AcaoProbabilidade;
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
            
            if (this.PossoPagarValor(this.Config.Ant + this.Config.Flop) && HaJogosParaJogar)
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

            if (minhaProbAgora >= this.AcaoProbabilidade.probabilidadeMinicaSeeFlop)
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

            if ((this.getMinhaProbalidadeAgora(this.JogadorStack.Mao, cartasMesa) >= this.AcaoProbabilidade.probabilidadeMinimaRaisePreTurn) && this.PossoPagarValor(this.Config.Turn))
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

            if ((this.getMinhaProbalidadeAgora(this.JogadorStack.Mao, cartasMesa) >= this.AcaoProbabilidade.probabilidadeMinimaRaisePreRiver) && this.PossoPagarValor(this.Config.River))
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
            return this.RetornaProbabilidade.GetProbabilidadeVitoria(minhasCartas, cartasMesa);
        }
    }
}
