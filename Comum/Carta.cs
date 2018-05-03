using Comum;
using Enuns;
using System;

namespace Modelo
{
    public sealed class Carta
    {
        public uint Numero { get; set; }
        public Naipe Naipe { get; set; }

        public Carta(uint numero, Naipe naipe)
        {
            if (numero < 0 || numero > 14) throw new Exception("O número da cartá é inválido.");
            Numero = numero;
            Naipe = naipe;
        }

        public int CompareTo(Carta other)
        {
            if (Numero < other.Numero) return -1;
            else if (Numero == other.Numero) return 0;

            return 1;
        }

        public override string ToString() =>
            Numero + "_" + Uteis.GetFirstDisplayNameEnum(Naipe) + " ";

        public Carta Clone() => new Carta (this.Numero, this.Naipe);
    }
}
