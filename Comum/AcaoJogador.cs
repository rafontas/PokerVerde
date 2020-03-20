using Comum.Interfaces;
using Enuns;

namespace Modelo
{
    public class AcaoJogador : IAcaoTomada
    {
        public uint Sequencial { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public IAcoesDecisao AcaoDecisao { get; set; }
        public uint ValorAcaoTomada { get; private set; }
        public AcoesDecisaoJogador Acao { get; set; }
        public AcaoJogador(AcoesDecisaoJogador acao, uint valor, IAcoesDecisao quemTomouAcao)
        {
            this.Acao = acao;
            this.ValorAcaoTomada = valor;
            this.AcaoDecisao = quemTomouAcao;
        }
    }
}
