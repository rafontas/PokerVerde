using Comum;

namespace Modelo
{
    public interface IDealerMesa
    {
        Mesa Mesa { get; }

        bool HaParticipantesParaJogar();
        void IniciarNovaPartida(IJogador j);
        void ExecutarProximaRodadaPartidaAtual(IJogador j);
        void TerminarPartidaAtual(IJogador j);
        
        void ExecutarNovaPartidaCompleta(IJogador j);

        void ExecutarCorrida(IJogador jogador);
        
        void IniciarNovaPartida();
        void ExecutarProximaRodadaPartidaAtual();
        void TerminarPartidaAtual();
        void ExecutarNovaPartidaCompleta();

        void ExecutarCorrida();
        void ExecutaTodasCorridas();
    }
}
