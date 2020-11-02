using Comum.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes
{
    public class ProbabilidadeMaoInicial : IProbabilidadeMaoInicial
    {
        public static HashSet<IProbabilidadeMaoInicial> ListaProbabilidade;
        
        public int Id { get; private set; }

        public static float GetProbabilidadeVitoria(Carta[] Mao)
        {
            IProbabilidadeMaoInicial p;

            if (ProbabilidadeMaoInicial.ListaProbabilidade == null || 
                ProbabilidadeMaoInicial.ListaProbabilidade.Count == 0 ||
                !ListaProbabilidade.TryGetValue(ProbabilidadeMaoInicial.getMe(Mao), out p))
                return 0.0f;

            return p.ProbabilidadeVitoria;
        }

        public ProbabilidadeMaoInicial() { }

        public ProbabilidadeMaoInicial(int Id) {
            this.Id = Id;
        }

        public uint NumCarta1 { get; set; }

        public uint NumCarta2 { get; set; }

        public char OffOrSuited { get; set; }

        public float ProbabilidadeVitoria { get; set; }
        
        public float ProbabilidadeSair { get; set; }

        public uint QuantidadesJogosSimulados { get; set; }

        public bool Equals(IProbabilidadeMaoInicial other)
        {
            if (this.Id == other.Id) return true;

            if (this.NumCarta1 == other.NumCarta1) {
                if (this.NumCarta2 != other.NumCarta2) return false;
            }
            else if(this.NumCarta2 == other.NumCarta1)
            {
                if (this.NumCarta1 != other.NumCarta2) return false;
            }

            if (this.OffOrSuited != other.OffOrSuited) return false;

            return true;
        }

        public static IProbabilidadeMaoInicial getMe(Carta[] other) 
        {
            IProbabilidadeMaoInicial p = new ProbabilidadeMaoInicial()
            {
                NumCarta1 = other[0].Numero,
                NumCarta2 = other[1].Numero,
                OffOrSuited = (other[0].Naipe == other[1].Naipe) ? 'S' : 'O'
            };

            return p;
        }

        public bool Equals(IProbabilidadeMaoInicial x, IProbabilidadeMaoInicial y)
        {
            if (((x.NumCarta1 == y.NumCarta1 && x.NumCarta2 == y.NumCarta2) ||
                    (x.NumCarta1 == y.NumCarta2 && x.NumCarta2 == y.NumCarta1)
                )
                && (x.OffOrSuited == y.OffOrSuited))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(IProbabilidadeMaoInicial obj)
        {
            string meuhash = "";

            if (obj.NumCarta1 <= obj.NumCarta2)
                meuhash = obj.NumCarta1.ToString() + obj.NumCarta2.ToString();
            else
                meuhash = obj.NumCarta2.ToString() + obj.NumCarta1.ToString();

            meuhash += obj.OffOrSuited;

            return meuhash.GetHashCode();
        }
    }
}
