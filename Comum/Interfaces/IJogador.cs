
using Comum.Interfaces;
using Enuns;
using System.Collections;
using System.Collections.Generic;

namespace Modelo
{
    public interface IJogador : IAcoesBasicasJogador, IEqualityComparer<IJogador>
    {
        IList<IAcoesDecisao> Mente { get; }
        AcaoJogador ExecutaAcao(TipoRodada momento, uint valorPagar, Carta[] mesa);
        void AddPartidaHistorico(IPartida p);
        ICorrida Corrida { get; set; }
        uint SeqProximaPartida { get; }
        IList<IPartida> Historico { get; }

        AcaoJogador PreJogo(uint valor);
        AcaoJogador PreFlop(uint valor);
        AcaoJogador Flop(Carta[] cartasMesa, uint valor);
        AcaoJogador Turn(Carta[] cartasMesa, uint valor);
        AcaoJogador River(Carta[] cartasMesa);
        AcaoJogador FimDeJogo();
    }
}
