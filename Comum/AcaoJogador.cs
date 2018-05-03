using Enuns;

namespace Modelo
{
    public class AcaoJogador
    {
        public TipoAcao Acao { get; set; }
        public uint Valor { get; set; } = 0;
        public MomentoJogo Momento { get; set; }

        public AcaoJogador(TipoAcao acao, uint valor, MomentoJogo momento)
        {
            Acao = acao;
            Valor = valor;
            Momento = momento + 1;
        }
    }
}
