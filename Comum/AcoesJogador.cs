using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Comum.AcoesJogador
{
    public enum AcoesPreJogo
    {
        [Display(Name = "Jogar")]
        Play = 1,
        [Display(Name = "Parar")]
        Stop = 2,
    }

    public enum AcoesPreFlop
    {
        [Display(Name = "Fugir")]
        Fold = 3,
        [Display(Name = "Pagar Flop")]
        PayFlop = 8,
    }

    public enum AcoesPreTurn
    {
        [Display(Name = "Fugir")]
        Fold = 3,
        [Display(Name = "Check")]
        Check = 4,
        [Display(Name = "Chamar")]
        Call = 5,
        [Display(Name = "Aumentar")]
        Raise = 6,
    }

    public enum AcoesPreRiver
    {
        [Display(Name = "Fugir")]
        Fold = 3,
        [Display(Name = "Check")]
        Check = 4,
        [Display(Name = "Chamar")]
        Call = 5,
        [Display(Name = "Aumentar")]
        Raise = 6,
    }

    public enum AcoesRiver
    {
        [Display(Name = "Fugir")]
        Fold = 3,
        [Display(Name = "Check")]
        Check = 4,
        [Display(Name = "Chamar")]
        Call = 5,
        [Display(Name = "Aumentar")]
        Raise = 6,
    }
}
