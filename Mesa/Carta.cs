using Modelo.Excecoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    public class Carta
    {
        public uint Numero { get; set; }
        public Naipe Naipe { get; set; }

        public Carta(uint numero, Naipe naipe)
        {
            if (numero < 0 || numero > 14) throw new CartaInvalidaException("O número da cartá é inválido.");
            this.Numero = numero;
            this.Naipe = naipe;
        }
    }
}
