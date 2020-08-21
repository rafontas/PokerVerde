using Comum;
using Comum.Interfaces;

namespace Modelo
{
    public interface ICroupier
    {
        Mesa Mesa { get; }

        IDealerPartida DealerPartida { get; set;  }

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
