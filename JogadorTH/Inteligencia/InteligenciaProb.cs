using Comum;
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

        public static bool ModoVerdoso { get; set; } = true;
        private AcaoJogador acaoJogadorAgora { get; set; }
        private float minhaProbAgora { get; set; }
        private TipoRodada rodadaAgora { get; set; }
        private string infoRadadaAgora { get; set; } = string.Empty;
        private string infoProbabilidade { get; set; } = string.Empty;
        private string infoAgora { get; set; } = string.Empty;

        public IRetornaProbabilidade RetornaProbabilidade { get; set; }

        internal IAcaoProbabilidade AcaoProbabilidade { get; set; }

        public InteligenciaProb(IAcaoProbabilidade AcaoProbabilidade, IRetornaProbabilidade RetornaProbabilidade) : base()
        {
            this.RetornaProbabilidade = RetornaProbabilidade;
            this.AcaoProbabilidade = AcaoProbabilidade;
        }

        public void ImprimeDados()
        {
            if (!Uteis.ModoVerboso) return;

            if (this.rodadaAgora == TipoRodada.PreJogo)
            {
                this.infoProbabilidade = string.Empty;
                this.infoRadadaAgora = " " + this.JogadorStack.Stack.ToString("0,00") + " - ";
            }
            else if (this.rodadaAgora == TipoRodada.PreFlop)
            {
                this.infoProbabilidade += string.Format(" PF {0} {1}", (((int)minhaProbAgora).ToString()), this.acaoJogadorAgora.Acao.ToString());
                this.infoRadadaAgora += this.GetMinhaMao(); 
            }
            else if (this.rodadaAgora == TipoRodada.Flop)
            {
                this.infoProbabilidade += string.Format(", F {0} {1}", (((int)minhaProbAgora).ToString()), this.acaoJogadorAgora.Acao.ToString());
                this.infoRadadaAgora += this.infoAgora; 
            }
            else if (this.rodadaAgora == TipoRodada.Turn)
            {
                this.infoProbabilidade += string.Format(", T {0} {1}", (((int)minhaProbAgora).ToString()), this.acaoJogadorAgora.Acao.ToString());
                this.infoRadadaAgora += this.infoAgora; 
            }
            else if (this.rodadaAgora == TipoRodada.River)
            {
                //this.infoProbabilidade += string.Format(", R {0} {1}", (minhaProbAgora.ToString("00,00")), this.acaoJogadorAgora.Acao.ToString());
                this.infoRadadaAgora += this.infoAgora;

                Uteis.ImprimeAgora = this.infoRadadaAgora + Uteis.ImprimeAgora;
                Uteis.ImprimeAgora += this.infoProbabilidade;
                //Console.Write(this.infoRadadaAgora + this.infoProbabilidade);
            }
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
            this.infoAgora = string.Empty;
            this.rodadaAgora = TipoRodada.PreJogo;
                 
            if (this.PossoPagarValor(this.Config.Ant + this.Config.Flop) && HaJogosParaJogar)
            {
                acao = AcoesDecisaoJogador.Play;
            }
            else
            {
                acao = AcoesDecisaoJogador.Stop;
            }

            this.ImprimeDados();

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

            this.infoAgora = string.Empty;
            this.acaoJogadorAgora = acao;
            this.minhaProbAgora = minhaProbAgora;
            this.rodadaAgora = TipoRodada.PreFlop;

            this.ImprimeDados();

            return acao;
        }

        public override AcaoJogador Flop(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador acao;
            float minhaProbAgora = this.getMinhaProbalidadeAgora(this.JogadorStack.Mao, cartasMesa);

            if ((minhaProbAgora >= this.AcaoProbabilidade.probabilidadeMinimaRaisePreTurn) && this.PossoPagarValor(this.Config.Turn))
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Raise, this.Config.Turn, this);
            }
            else
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);
            }
            this.infoAgora = "[" + cartasMesa[0].ToString() + " " + cartasMesa[1].ToString() + " " + cartasMesa[2].ToString();
            this.acaoJogadorAgora = acao;
            this.minhaProbAgora = minhaProbAgora;
            this.rodadaAgora = TipoRodada.Flop;

            this.ImprimeDados();

            return acao;
        }
        
        public override AcaoJogador Turn(Carta[] cartasMesa, uint valor)
        {
            AcaoJogador acao;
            float minhaProbAgora = this.getMinhaProbalidadeAgora(this.JogadorStack.Mao, cartasMesa);

            if ((minhaProbAgora >= this.AcaoProbabilidade.probabilidadeMinimaRaisePreRiver) && this.PossoPagarValor(this.Config.River))
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Raise, this.Config.River, this);
            }
            else
            {
                acao = new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);
            }

            this.infoAgora = " " + cartasMesa[3].ToString();
            this.acaoJogadorAgora = acao;
            this.minhaProbAgora = minhaProbAgora;
            this.rodadaAgora = TipoRodada.Turn;

            this.ImprimeDados();

            return acao;
        }

        public override AcaoJogador River(Carta[] cartasMesa)
        {
            AcaoJogador acao = new AcaoJogador(AcoesDecisaoJogador.Check, 0, this);
            
            this.infoAgora = " " + cartasMesa[4].ToString() + "]";
            this.rodadaAgora = TipoRodada.River;

            this.ImprimeDados();

            return acao;
        }

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
