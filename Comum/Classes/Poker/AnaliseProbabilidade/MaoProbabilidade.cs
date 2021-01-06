using Comum.Interfaces.AnaliseProbabilidade;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Comum.Classes.Poker.AnaliseProbabilidade
{
    public class MaoProbabilidade : IMaoProbabilidade
    {
        public int Id { get; set; }

        public IDictionary<Carta, Carta> CartasMao { get; set; }
        public IDictionary<Carta, Carta> CartasMesa { get; set; }

        public string HandTokenizada { get; set; }

        public string PocketHandTokenizada { get; set; }
        
        public float ProbabilidadeVitoria { get; set; }
        
        public MaoProbabilidade(int id, string maoTokenizada, float probVitoria) 
        {
            this.Id = id;
            this.HandTokenizada = maoTokenizada;
            this.PocketHandTokenizada = maoTokenizada.Substring(0, 5);
            this.ProbabilidadeVitoria = probVitoria;
       }

        public MaoProbabilidade(IList<Carta> MaoInicial)
        {
            this.CartasMao = new SortedList<Carta, Carta>();

            foreach (Carta c in MaoInicial) this.AddMao(c);
        }

        public MaoProbabilidade(Carta [] MaoInicial)
        {
            this.CartasMao = new SortedList<Carta, Carta>();

            foreach (Carta c in MaoInicial) this.AddMao(c);
        }

        public MaoProbabilidade(Carta [] mao, Carta [] mesa)
        {
            this.CartasMao = new SortedList<Carta, Carta>();
            this.CartasMesa = new SortedList<Carta, Carta>();

            foreach (Carta c in mao) this.AddMao(c);
            
            if(mesa != null)
                foreach (Carta c in mesa) this.AddMesa(c);
        }

        public MaoProbabilidade(string maoTokenizada, float probVitoria)
        {
            this.HandTokenizada = maoTokenizada;
            this.ProbabilidadeVitoria = probVitoria;
        }

        private string ToCartaTokenizada(Carta c, IDictionary<Naipe, uint> NaipeMap)
        {
            string carta = "";
            if (c.Numero >= 10 || c.Numero == 1)
            {
                switch (c.Numero)
                {
                    case 10:
                        carta = "T"; break;
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
                carta = c.Numero.ToString();
            }

            carta += this.GetTokenNaipe(c.Naipe, NaipeMap);

            return carta;
        }

        private string GetTokenNaipe(Naipe n, IDictionary<Naipe, uint> NaipeMap)
        {
            if (NaipeMap.ContainsKey(n)) return NaipeMap[n].ToString();

            uint valorNaipe = 0;

            if (NaipeMap.Count > 0) valorNaipe = (NaipeMap.Last().Value + 1);

            NaipeMap.Add(n, valorNaipe);

            return valorNaipe.ToString();
        }

        public string ToMaoTokenizada()
        {
            if (CartasMao.Count == 0) return string.Empty;

            string retorno = "";
            IDictionary<Naipe, uint> NaipeMap = new Dictionary<Naipe, uint>();

            retorno = this.ToCartaTokenizada(CartasMao.First().Value, NaipeMap);

            for(int i = 1; i < this.CartasMao.Count; i++)
            {
                retorno += " " + this.ToCartaTokenizada(CartasMao.ElementAt(i).Value, NaipeMap);
            }

            this.PocketHandTokenizada = String.Copy(retorno);

            for(int i = 0; i < this.CartasMesa.Count; i++)
            {
                retorno += " " + this.ToCartaTokenizada(CartasMesa.ElementAt(i).Value, NaipeMap);
            }

            this.HandTokenizada = retorno;
            return retorno;
        }

        public void AddMesa(Carta c) {
            if (c == null) return;
            this.CartasMesa.Add(c, c);
        }
        public void AddMao(Carta c) {
            if (c == null) return;
            this.CartasMao.Add(c, c);
        }

        private static string GetPocketHand (int numero1, int numero2, char OffSuited)
        {
            string 
                pocketHand = Carta.NumberToCardNumber(numero1),
                naipe1 = "0", 
                naipe2 = "0";

            if (OffSuited == 'O')
            {
                naipe2 = "1";
            }

            return string.Format("{0}{1} {2}{3}",
                Carta.NumberToCardNumber(numero1),
                naipe1,
                Carta.NumberToCardNumber(numero2),
                naipe2);
        }

        public static IDictionary<string, IDictionary<string, float>> GetPocketHands()
        {
            string KeyDic = string.Empty;
            string naipe1 = "0", naipe2 = "1";
            IDictionary<string, IDictionary<string, float>> DicionarioProbabilidade 
                = new Dictionary<string, IDictionary<string, float>>();

            for (int i = 2; i <= 14; i++)
            {

                for (int j = i; j <= 14; j++)
                {   
                    KeyDic = MaoProbabilidade.GetPocketHand(i, j, 'O');
                    DicionarioProbabilidade.Add(KeyDic, new Dictionary<string, float>());

                    if(i != j)
                    {
                        KeyDic = MaoProbabilidade.GetPocketHand(i, j, 'S');
                        DicionarioProbabilidade.Add(KeyDic, new Dictionary<string, float>());
                    }
                }
            }

            return DicionarioProbabilidade;
        }
    }
}
