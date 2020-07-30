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
            if (!this.PossoPagarValor(ValorHaPagar)) throw new JogadorException("Jogador sem stack para efetuar pagamento");

            this._Stack -= ValorHaPagar;
            return ValorHaPagar;
        }

        public void ReceberValor(uint ValorHaReceber) => this._Stack += ValorHaReceber;

        public bool PossoPagarValor(uint ValorAhPagar) => (this._Stack >= ValorAhPagar);
    }
}
