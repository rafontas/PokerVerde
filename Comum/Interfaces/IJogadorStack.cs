using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IJogadorStack
    {
        uint StackInicial { get; }

        uint Stack { get; }
        
        Carta [] Mao { get; }

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

        /// <summary>
        /// Recebe cartas
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        void ReceberCarta(Carta c1, Carta c2);

        /// <summary>
        /// Desatribui as cartas da mao
        /// </summary>
        void ResetaMao();
    }
}
