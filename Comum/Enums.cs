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
        StraightFlush = 8
    }

    public enum TipoAcao
    {
        SemAcao,
        Check,
        Call,
        Raise,
        Play,
        Fold,
        Stop
    }

    public enum MomentoJogo
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
        SemJogador = 0,
        Jogador = 1,
        Mesa = 2
    }

}
