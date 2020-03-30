using Comum.Interfaces;
using Enuns;

namespace Modelo
{
    public class AcaoJogador : IAcaoTomada
    {
        private static uint seq { 
            get { return AcaoJogador.seq++; } 
            set { AcaoJogador.seq = value; } 
        } 
        public uint Sequencial { get; set; }
        public IAcoesDecisao AcaoDecisao { get; set; }
        public uint ValorAcaoTomada { get; private set; }
        public AcoesDecisaoJogador Acao { get; set; }
        public AcaoJogador(AcoesDecisaoJogador acao, uint valor, IAcoesDecisao quemTomouAcao, uint seq = 0)
        {
            this.Acao = acao;
            this.ValorAcaoTomada = valor;
            this.AcaoDecisao = quemTomouAcao;
            this.Sequencial = (seq == 0 ? AcaoJogador.seq : seq);
        }
    }
}
