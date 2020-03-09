using Enuns;

namespace Modelo
{
    public class MomentoJogoControle
    {
        public TipoRodada MomentoAtual { get; private set; } = TipoRodada.PreJogo;

        /// <summary>
        /// Avança para o próximo momento do jogo.
        /// </summary>
        public TipoRodada Proximo() => ++MomentoAtual;

        /// <summary>
        /// Termina o jogo se o Jogador foldar.
        /// </summary>
        public void TerminaJogo() => MomentoAtual = TipoRodada.FimDeJogo;
    }
}
