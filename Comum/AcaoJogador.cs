using Comum.Interfaces;
using Enuns;

namespace Modelo
{
    public class AcaoJogador : IAcaoTomada
    {
        private static uint _seq = 0;
        private static uint seq { 
            get { return AcaoJogador._seq++; } 
            set { AcaoJogador._seq = value; } 
        } 
        public uint Sequencial { get; set; }
        public IAcoesDecisao AcaoDecisao { get; set; }
        public uint ValorAcaoTomada { get; private set; }
        public AcoesDecisaoJogador Acao { get; set; }

        public uint ValorRequerido => throw new System.NotImplementedException();

        public uint ValorDaAcaoTomada => throw new System.NotImplementedException();

        public AcaoJogador(AcoesDecisaoJogador acao, uint valor, IAcoesDecisao quemTomouAcao, uint seq = 0)
        {
            this.Acao = acao;
            this.ValorAcaoTomada = valor;
            this.AcaoDecisao = quemTomouAcao;
            this.Sequencial = (seq == 0 ? AcaoJogador.seq : seq);
        }

        public IAcaoTomada Clone() => new AcaoJogador(this.Acao, this.ValorAcaoTomada, this.AcaoDecisao, this.Sequencial);
    }
}
