using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IDealerPartida
    {
        Mesa Mesa { get; }
        
        bool HaJogadoresParaJogar();
        bool ExistePartidaEmAndamento();
        void PrepararNovaPartida();
        void PergutarQuemIraJogar();

        void CobrarAnt(IJogador j);
        void DistribuirCartasJogadores(IPartida p);
        void ExecutarPreFlop();
        void PerguntarPagarFlop();
        void RevelarFlop();
        void PerguntarAumentarPreTurn();
        void RevelarTurn();
        void PerguntarAumentarPreRiver();
        void RevelarRiver();
        void EncerrarPartidas();

        void EntregarPoteAosVencedores(IPartida p);

        void EncerrarPartidaJogador(IJogador j);
        
        /// <summary>
        /// Retira jogador da mesa.
        /// </summary>
        /// <param name="j">Jogador a sair.</param>
        void RetirarJogadorDaMesa(IJogador j);

        void VerificarGanhadorPartida(IPartida p);

        IJogador GetBancaPadrao();
    }
}
