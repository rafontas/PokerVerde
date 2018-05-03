using Modelo.Excecoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class Jogador
    {
        public string Nome { get; set; }

        public Carta [] Cartas { get; set; }

        public TipoPoker Tipo { get; set; }

        public int Valor { get; set; }

        public Jogador(TipoPoker tipo)
        {
            if (tipo == TipoPoker.TexasHoldem) this.Cartas = new Carta[2];
        }

        /// <summary>
        /// Atribiu as cartas ao jogador.
        /// </summary>
        /// <param name="cartas"></param>
        public void RecebeCartas(IList<Carta> cartas)
        {
            if (cartas.Count != this.Cartas.Length)
                throw new JogadorExcpetion("O jogador recebeu mais/menos cartas que sua mão permite.");

            for (int i = 0; i < Cartas.Length; i++) Cartas[i] = cartas[i];
        }

        /// <summary>
        /// Zera as cartas do jogadors
        /// </summary>
        public void ResetaMao()
        {
            for (int i = 0; i < this.Cartas.Length; i++)
                this.Cartas[i] = null;
        }
    }
}
