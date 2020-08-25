
using Comum.Interfaces;
using Enuns;
using System.Collections;
using System.Collections.Generic;

namespace Modelo
{
    public interface IJogador : IAcoesBasicasJogador, IEqualityComparer<IJogador>
    {
        uint SeqProximaPartida { get; }
        bool VouJogarMaisUmaPartida();

        void AddPartidaHistorico(IPartida p);
        IList<IAcoesDecisao> Mente { get; }
        IList<IPartida> Historico { get; }
        ICorrida Corrida { get; set; }
        IJogadorEstatisticas Estatistica { get; }
        IJogadorStack JogadorStack { get; }
        IList<IImprimePartida> ImprimePartida { get; }
        IConfiguracaoPoker ConfiguracaoPoker { get; set; }

        AcaoJogador ExecutaAcao(TipoRodada momento, uint valorPagar, Carta[] mesa);
        AcaoJogador PreJogo(uint valor);
        AcaoJogador PreFlop(uint valor);
        AcaoJogador Flop(Carta[] cartasMesa, uint valor);
        AcaoJogador Turn(Carta[] cartasMesa, uint valor);
        AcaoJogador River(Carta[] cartasMesa);
        AcaoJogador FimDeJogo();
    }
}
