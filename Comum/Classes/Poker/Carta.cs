using Comum;
using Comum.Interfaces;
using Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelo
{
    public class Carta : IEqualityComparer<Carta>, IToJson
    {

        [Display(Name = "Numero")]
        public uint Numero { get; set; }

        [Display(Name = "Naipe")]
        public Naipe Naipe { get; set; }

        public Carta() { }

        public Carta(uint numero, Naipe naipe)
        {
            if (numero <= 0 || numero > 14) throw new Exception("O número da cartá é inválido.");
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
            Numero + "_" + Uteis.GetFirstDisplayNameEnum(Naipe);

        public Carta Clone() => new Carta(this.Numero, this.Naipe);

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

        public string ToJson()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.AppendFormat("\t\"Numero\":\"{0}\"", this.Numero);
            stringBuilder.Append("," + Environment.NewLine);
            stringBuilder.AppendFormat("\t\"Naipe\":\"{0}\"", Uteis.GetFirstDisplayNameEnum(this.Naipe));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        public static Carta FromJson(string Json)
        {
            int indiceAux;
            string meuNumeroStr = string.Empty, meuNaipeStr = string.Empty;
            uint meuNumero;
            Enuns.Naipe meuNaipe;

            indiceAux = Json.IndexOf(':');
            indiceAux++; indiceAux++;

            while (Json[indiceAux] != '"')
            {
                meuNumeroStr += Json[indiceAux];
                indiceAux++;
            }

            indiceAux = Json.LastIndexOf(':');
            indiceAux++; indiceAux++;

            while (Json[indiceAux] != '"')
            {
                meuNaipeStr += Json[indiceAux];
                indiceAux++;
            }

            meuNumero = uint.Parse(meuNumeroStr);
            switch(meuNaipeStr) 
            {
                case "♥": meuNaipe = Naipe.Copas; break;
                case "♦": meuNaipe = Naipe.Ouros; break;
                case "♠": meuNaipe = Naipe.Espadas; break;
                case "♣": meuNaipe = Naipe.Paus; break;
                default: throw new Exception("Naipe não existente.");
            }

            return new Carta(meuNumero, meuNaipe);
        }
        
        public static Carta FromString(string stringObject)
        {
            int indiceAux = 0;
            string meuNumeroStr = string.Empty, meuNaipeStr = string.Empty;
            uint meuNumero;
            Enuns.Naipe meuNaipe;

            while (stringObject[indiceAux] != '_')
            {
                meuNumeroStr += stringObject[indiceAux];
                indiceAux++;
            }

            indiceAux++;
            meuNaipeStr += stringObject[indiceAux];

            meuNumero = uint.Parse(meuNumeroStr);
            switch (meuNaipeStr)
            {
                case "♥": meuNaipe = Naipe.Copas; break;
                case "♦": meuNaipe = Naipe.Ouros; break;
                case "♠": meuNaipe = Naipe.Espadas; break;
                case "♣": meuNaipe = Naipe.Paus; break;
                default: throw new Exception("Naipe não existente.");
            }

            return new Carta(meuNumero, meuNaipe);
        }
    }
}
