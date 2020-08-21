using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Enuns
{
    public enum Naipe {
        [Display(Name = "♥")]
        Copas = 1,
        [Display(Name = "♦")]
        Ouros = 2,
        [Display(Name = "♠")]
        Espadas = 3,
        [Display(Name = "♣")]
        Paus = 4,
    }

    public enum Jogo
    {
        Nada = 0,
        CartaAlta = 1,
        Dupla = 2,
        DuasDuplas = 3,
        Trinca = 4,
        Straight = 5,
        Flush = 6,
        FullHand = 7,
        Four = 8,
        StraightFlush = 9
    }

    public enum VencedorPartida
    {
        [Display(Name = "E")]
        Empate = 1,
        [Display(Name = "B")]
        Banca = 2,
        [Display(Name = "J")]
        Jogador = 3
    }

    public enum AcoesDecisaoJogador
    {
        [Display(Name = "Jogar")]
        Play = 1,
        [Display(Name = "Parar")]
        Stop = 2,
        [Display(Name = "Fugir")]
        Fold = 3,
        [Display(Name = "Check")]
        Check = 4,
        [Display(Name = "Chamar")]
        Call = 5,
        [Display(Name = "Aumentar")]
        Raise = 6,
        [Display(Name = "Pagar")]
        Pay = 7,
        [Display(Name = "Pagar Flop")]
        PayFlop = 8,
    }

    public enum TipoRodada
    {
        [Display(Name = "Pré Jogo")]
        PreJogo = 1,
        [Display(Name = "Pré Flop")]
        PreFlop = 2,
        [Display(Name = "Flop")]
        Flop = 3,
        [Display(Name = "Turn")]
        Turn = 4,
        [Display(Name = "River")]
        River = 5,
        [Display(Name = "Terminou Rodada")]
        FimDeJogo = 6
    }

    public enum TipoJogadorTHB
    {
        Jogador = 1,
        Banca = 2
    }

}
