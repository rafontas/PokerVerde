using Comum.Excecoes;
using Comum.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes
{
    public class JogadorStack : IJogadorStack
    {
        private uint _StackInicial { get; set; }
        public uint StackInicial { get => this._StackInicial; }
        private uint _Stack { get; set; }
        public uint Stack { get => this._Stack; }

        private Carta[] cartas { get; set; } = new Carta[] { null, null };

        public Carta[] Mao { get => this.cartas; }

        public void ResetaMao() => this.cartas = new Carta[] { null, null };

        public void ReceberCarta(Carta c1, Carta c2)
        {
            this.cartas[0] = c1;
            this.cartas[1] = c2;
        }

        public JogadorStack(uint Stack)
        {
            this._StackInicial = Stack;
            this._Stack = Stack;
        }

        public uint PagarValor(uint ValorHaPagar)
        {
            if (!this.PossoPagarValor(ValorHaPagar)) throw new JogadorException("Jogador sem stack para efetuar pagamento");

            this._Stack -= ValorHaPagar;
            return ValorHaPagar;
        }

        public void ReceberValor(uint ValorHaReceber) => this._Stack += ValorHaReceber;

        public bool PossoPagarValor(uint ValorAhPagar) => (this._Stack >= ValorAhPagar);
    }
}
