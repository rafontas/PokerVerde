using Comum.Excecoes;
using Comum.Interfaces;
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

        public JogadorStack(uint Stack)
        {
            this._StackInicial = Stack;
            this._Stack = Stack;
        }

        public uint PagarValor(uint ValorHaPagar)
        {
            if (this._Stack < ValorHaPagar) throw new JogadorException("O jogador não tem este valor para pagar");

            this._Stack -= ValorHaPagar;
            return this.Stack;
        }

        public uint ReceberValor(uint ValorHaReceber) => (this._Stack += ValorHaReceber);

        public bool PossoPagarValor(uint Valor) => (this._Stack >= Valor);
    }
}
