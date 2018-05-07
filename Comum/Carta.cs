using Comum;
using Enuns;
using System;
using System.Collections.Generic;

namespace Modelo
{
    public sealed class Carta : IEqualityComparer<Carta>
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;

            return (Numero == ((Carta)obj).Numero && Naipe == ((Carta)obj).Naipe);
        }

        public bool Equals(Carta x, Carta y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

            return (x.Numero == y.Numero && x.Naipe == y.Naipe);
        }

        public override int GetHashCode() => Numero.GetHashCode() ^ Naipe.GetHashCode();

        public int GetHashCode(Carta obj)
        {
            if (ReferenceEquals(obj, null)) return 0;
            return obj.Numero.GetHashCode() ^ obj.Naipe.GetHashCode();
        }
    }
}
