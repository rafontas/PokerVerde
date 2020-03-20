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

        void ExecutaCorrida(IJogador jogador);
        
        void IniciarNovaPartida();
        void ExecutarProximaRodadaPartidaAtual();
        void TerminarPartidaAtual();
        void ExecutarNovaPartidaCompleta();

        void ExecutaCorrida();
        void ExecutaTodasCorridas();
    }
}
