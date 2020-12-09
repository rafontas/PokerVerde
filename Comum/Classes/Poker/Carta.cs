using Comum;
using Comum.Interfaces;
using Comum.Interfaces.PokerBase;
using Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelo
{
    public class Carta : IEqualityComparer<Carta>, IToJson, IToFastCard
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

        public override string ToString()
        {
            string carta = "";
            if (this.Numero > 10) 
            {
                switch(this.Numero)
                {
                    case 11: 
                        carta = "J"; break;
                    case 12: 
                        carta = "Q"; break;
                    case 13: 
                        carta = "K"; break;
                    case 14:
                    case 1:
                        carta = "A"; break;
                    default: 
                        throw new Exception("Numero não identificado na conversão;");
                }
            }
            else
            {
                carta = this.Numero.ToString();
            }
                 
            carta += Uteis.GetFirstDisplayNameEnum(Naipe);
            return carta;
        }

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

        public static Enuns.Naipe GetNaipeAleatorio()
        {
            Random rnd = new Random();
            int NumUm = rnd.Next(0, 3);

            switch(NumUm)
            {
                case 0: return Naipe.Copas;
                case 1: return Naipe.Ouros;
                case 2: return Naipe.Paus;
                case 3: return Naipe.Espadas;
                default: throw new Exception("Naipe não existe!");
            }
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

        public string ToFastCard()
        {
            string card = "";
            
            switch(this.Numero)
            {
                case 0: card = "a"; break;
                case 2: card = "2"; break;
                case 3: card = "3"; break;
                case 4: card = "4"; break;
                case 5: card = "5"; break;
                case 6: card = "6"; break;
                case 7: card = "7"; break;
                case 8: card = "8"; break;
                case 9: card = "9"; break;
                case 10: card = "t"; break;
                case 11: card = "j"; break;
                case 12: card = "q"; break;
                case 13: card = "k"; break;
                case 14: card = "a"; break;
                default: throw new Exception("Numero da carta não encontrado");
            }

            switch(this.Naipe)
            {
                case Naipe.Copas: card = card + "h"; break;
                case Naipe.Espadas: card = card + "s"; break;
                case Naipe.Ouros: card = card + "d"; break;
                case Naipe.Paus: card = card + "c"; break;
                default: throw new Exception();
            }

            return card;
        }
    }
}
