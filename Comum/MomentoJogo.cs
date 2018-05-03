using Enuns;

namespace Modelo
{
    public class MomentoJogoControle
    {
        public MomentoJogo MomentoAtual { get; private set; } = MomentoJogo.PreJogo;

        /// <summary>
        /// Avança para o próximo momento do jogo.
        /// </summary>
        public MomentoJogo Proximo() => ++MomentoAtual;

        /// <summary>
        /// Termina o jogo se o Jogador foldar.
        /// </summary>
        public void TerminaJogo() => MomentoAtual = MomentoJogo.FimDeJogo;
    }
}
