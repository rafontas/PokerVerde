using Enuns;

namespace Modelo
{
    public class AcaoJogador
    {
        public TipoAcao Acao { get; set; }
        public uint Valor { get; set; } = 0;
        public TipoRodada Momento { get; set; }
        public uint Sequencial { get; set; }
        public AcaoJogador(TipoAcao acao, uint valor, TipoRodada momento)
        {
            Acao = acao;
            Valor = valor;
            Momento = momento + 1;
        }
    }
}
