using Comum.Interfaces.AnaliseProbabilidade;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes.Poker.AnaliseProbabilidade
{
    public class MaoBasica : IMaoBasica
    {
        public uint NumCarta1 { get; set; }
        public uint NumCarta2 { get; set; }
        public char OffOrSuited { get; set; }
        public IMaoBasica Clone() => new MaoBasica() { NumCarta1 = this.NumCarta1, NumCarta2 = this.NumCarta2, OffOrSuited = this.OffOrSuited };

        public bool Equals(IMaoBasica other)
        {
            if (this.OffOrSuited != other.OffOrSuited) return false;

            if ((this.NumCarta1 == other.NumCarta1  && this.NumCarta2 == other.NumCarta2) ||
                (this.NumCarta1 == other.NumCarta2 && this.NumCarta2 == other.NumCarta1))
            {
                return true;
            }

            return false;
        }
    
        public static IList<IMaoBasica> GetTodasMaosPossiveis()
        {
            IList<IMaoBasica> lista = new List<IMaoBasica>();

            for (uint i = 2; i <= 14; i++)
            {
                lista.Add(new MaoBasica()
                {
                    NumCarta1 = i,
                    NumCarta2 = i,
                    OffOrSuited = 'O'
                });

                for (uint j = (i+1); j <= 14; j++)
                {
                    lista.Add(new MaoBasica()
                    {
                        NumCarta1 = i,
                        NumCarta2 = j,
                        OffOrSuited = 'S'
                    });
                    lista.Add(new MaoBasica()
                    {
                        NumCarta1 = i,
                        NumCarta2 = j,
                        OffOrSuited = 'O'
                    });
                }
            }

            return lista;
        }

        public static IMaoBasica ToMao(Carta c1, Carta c2) 
        {
            IMaoBasica mao = new MaoBasica();

            mao.NumCarta1 = c1.Numero;
            mao.NumCarta2 = c2.Numero;
            mao.OffOrSuited = 'O';

            if (c1.Naipe == c2.Naipe)
            {
                mao.OffOrSuited = 'S';
            }

            return mao;
        }

        public override int GetHashCode()
        {
            string hash = string.Empty;

            if (this.NumCarta1 < NumCarta2)
            {
                hash = NumCarta1 + " " + NumCarta2;
            }
            else
            {
                hash = NumCarta2 + " " + NumCarta1;
            }

            hash = hash + " " + OffOrSuited;

            return hash.GetHashCode();
        }

    }
    
}
