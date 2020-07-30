using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IJogadorStack
    {
        uint StackInicial { get; }
        uint Stack { get; }
        
        /// <summary>
        /// Retira o valor do stack do jogador e o retorna.
        /// </summary>
        /// <param name="ValorHaPagar">Valor a ser pago</param>
        /// <returns>Valor pago</returns>
        uint PagarValor(uint ValorHaPagar);
        
        /// <summary>
        /// Adiciona valor ao stack do jogador
        /// </summary>
        /// <param name="ValorHaReceber"></param>
        void ReceberValor(uint ValorHaReceber);
        
        /// <summary>
        /// Indica se o jogador pode pagar ou não um valor
        /// </summary>
        /// <param name="Valor">Valor a ser pago</param>
        /// <returns>Pode ou não pagar</returns>
        bool PossoPagarValor(uint Valor);
    }
}
