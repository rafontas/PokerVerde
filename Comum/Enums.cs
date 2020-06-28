using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Enuns
{
    public enum Naipe {
        [Display(Name = "C")]
        Copas = 1,
        [Display(Name = "O")]
        Ouros = 2,
        [Display(Name = "E")]
        Espadas = 3,
        [Display(Name = "P")]
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

    public enum ActionPlayer
    {
        Play = 1,
        Stop = 2,
        Fold = 3,
        Check = 4,
        Call = 5,
        Raise = 6,
    }

    public enum VencedorPartida
    {
        Empate = 1,
        Banca = 2,
        Jogador = 3
    }

    public enum ActionDealer
    {
        [Display(Name = "Pegunta se jogará")]
        AskToPlay = 1,
        [Display(Name = "Dá cartas jogador")]
        DealPlayerCards = 2,
        [Display(Name = "Aumenta Flop")]
        AskRaisePreFlop = 3,
        [Display(Name = "Mostra Flop")]
        ShowFlop = 4,
        [Display(Name = "Apostará Flop")]
        AskRaiseFlop = 5,
        [Display(Name = "Mostra Turn")]
        ShowTurn = 6,
        [Display(Name = "Apostará Turn")]
        AskRaiseTurn = 7,
        [Display(Name = "Mostra River")]
        ShowRiver = 8,
        [Display(Name = "Apostará River")]
        AskRaiseRiver = 9,
        [Display(Name = "Dá cartas banca")]
        DealBankCards = 10,
        [Display(Name = "Verifica quem ganhou")]
        VerifyWinner = 11,
        [Display(Name = "Pede para pagar")]
        AsKToPay = 12,
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
        [Display(Name = "Pós River")]
        PosRiver = 6,
        [Display(Name = "Terminou Rodada")]
        FimDeJogo = 7
    }

    public enum TipoJogadorTHB
    {
        Jogador = 1,
        Banca = 2
    }

}
