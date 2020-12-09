using Comum.HoldemHand;
using Modelo;
using System.Collections.Generic;

namespace DealerTH.Probabilidade
{
    public class AvaliaProbabilidadeMao
    {
        internal class CartaA : Carta
        {
            internal uint RodadaNum { get; set; } = 0;

            internal CartaA ToCartaA(Carta C) => new CartaA() { Numero = C.Numero, Naipe = C.Naipe, RodadaNum = 0 };

        }

        internal class DeckA : Deck<CartaA> {

            internal DeckA() => this.CriaDeckAvaliacao();

            internal void CriaDeckAvaliacao(){
                Baralho = new List<CartaA>();
                for (uint i = 2; i <= 14; i++)
                {
                    Baralho.Add(new CartaA() { Numero = i, Naipe = Enuns.Naipe.Copas, RodadaNum = 0 });
                    Baralho.Add(new CartaA() { Numero = i, Naipe = Enuns.Naipe.Ouros, RodadaNum = 0 });
                    Baralho.Add(new CartaA() { Numero = i, Naipe = Enuns.Naipe.Espadas, RodadaNum = 0 });
                    Baralho.Add(new CartaA() { Numero = i, Naipe = Enuns.Naipe.Paus, RodadaNum = 0 });
                }

                Shuffle();
            }
        }

        const int LIMITE_MESA = 5;
        const int LIMITE_MAO_P = 2;
        const int LIMITE_MAO_S = 2;

        private uint NumeroRodadas { get; set; } 

        private DeckA Deck { get; set; }

        private CartaA [] MaoPrincipal { get; set; }
        private int NumCartasMao { get; set; }

        private CartaA [] MaoSecundaria { get; set; }
        private int NumCartasMaoSec { get; set; }

        private CartaA [] Mesa { get; set; }
        private int NumCartasMesa { get; set; }

        private void ToCartaA(IList<Carta> C, CartaA [] array)
        {
            if (C == null) return;

            int indice = 0;
            foreach(var c in C)
                array[indice++] = new CartaA() { Numero = c.Numero, Naipe = c.Naipe, RodadaNum = 0 };
        }

        public AvaliaProbabilidadeMao(IList<Carta> mao, IList<Carta> maoSecundaria, IList<Carta> mesa, uint numeroRodadas = 100000)
        {
            Deck = new DeckA();
            Deck.CriaDeckAvaliacao();

            MaoPrincipal = new CartaA[LIMITE_MAO_P];
            MaoSecundaria = new CartaA[LIMITE_MAO_S];
            Mesa = new CartaA[LIMITE_MESA];

            ToCartaA(mao, MaoPrincipal);
            ToCartaA(maoSecundaria, MaoSecundaria);
            ToCartaA(mesa, Mesa);
            
            NumCartasMao = mao?.Count ?? 0;
            NumCartasMaoSec = maoSecundaria?.Count ?? 0;
            NumCartasMesa = mesa?.Count ?? 0;

            NumeroRodadas = numeroRodadas;
        }

        internal void Avalia(out uint vitorias, out uint derrotas, out uint empate)
        {
            vitorias = 0;
            derrotas = 0;
            empate = 0;

            // Remove as cartas da mão e mesa, já que não está sendo utilizadas
            foreach (var c in MaoSecundaria) Deck.Remove(c);
            foreach (var c in MaoPrincipal) Deck.Remove(c);
            foreach (var c in Mesa) Deck.Remove(c);

            // Monta o limite de cartas que tem que ser preenchidas
            int limite = (LIMITE_MESA - NumCartasMesa) > (LIMITE_MAO_P - NumCartasMao) ?
                         (LIMITE_MESA - NumCartasMesa) : (LIMITE_MAO_P - NumCartasMao);

            limite = limite > (LIMITE_MAO_S - NumCartasMaoSec) ? limite : (LIMITE_MAO_S - NumCartasMaoSec);

            for (int i = 1; i <= NumeroRodadas; i++)
            {

                int j = NumCartasMesa, k = NumCartasMao, l = NumCartasMaoSec;

                // Preenhce a mao
                while (k < LIMITE_MAO_P) MaoPrincipal[k++] = GetCartaIneditaRodada(i);
                    
                // Preenche mão secundária
                while (l < LIMITE_MAO_S) MaoSecundaria[l++] = GetCartaIneditaRodada(i);

                // Preenhce a mesa
                while (j < LIMITE_MESA) Mesa[j++] = GetCartaIneditaRodada(i);

                //int resultado = mp.Compara(ms);
                int resultado = this.AvaliaMelhorMao(MaoPrincipal, MaoSecundaria, Mesa);

                if (resultado == 1) vitorias++;
                else if (resultado == -1) derrotas++;
                else empate++;
            }
        }

        private CartaA GetCartaIneditaRodada(int numRodada)
        {
            CartaA aux = Deck.CartaRandom;
            while (aux.RodadaNum == numRodada) aux = Deck.CartaRandom;
            aux.RodadaNum = (uint)numRodada;

            return aux;
        }

        private int AvaliaMelhorMao(Carta [] jogador1, Carta[] jogador2, Carta [] mesa) 
        {
            string m = mesa[0].ToFastCard() + " " + mesa[1].ToFastCard() + " " + mesa[2].ToFastCard() + " " + mesa[3].ToFastCard() + " " + mesa[4].ToFastCard() + " ",
                jog1 = jogador1[0].ToFastCard() + " " + jogador1[1].ToFastCard(),
                jog2 = jogador2[0].ToFastCard() + " " + jogador2[1].ToFastCard();
            
            int resultado;

            Hand maoJog1 = new Hand(jog1, m);
            Hand maoJog2 = new Hand(jog2, m);

            if (maoJog1 > maoJog2) resultado = 1;
            else if (maoJog1 < maoJog2) resultado = -1;
            else resultado = 0;

            return resultado;
        }

        /// <summary>
        /// Calcula a probabilidade de uma mão vencer.
        /// </summary>
        /// <param name="mao">Cartas da mão a ser avaliada. Podem ser passadas 1 ou 2 cartas.</param>
        /// <param name="numeroRodadas">Número rodadas simuladas para convergência de resultados. Com 150k já convergem. </param>
        /// <returns>Probabilidade de ganhar. Ex: 50.10</returns>
        public static float GetRecalculaVitoria(Carta[] mao, uint numeroRodadas = 150000) => AvaliaProbabilidadeMao.GetRecalculaVitoria(mao, null, null, numeroRodadas);

        /// <summary>
        /// Calcula a probabilidade de uma mão vencer.
        /// </summary>
        /// <param name="mao">Cartas da mão a ser avaliada. Pode ser 1 ou 2.</param>
        /// <param name="mesa">Cartas que já estão na mesa. De 0 a 5 cartas.</param>
        /// <param name="numeroRodadas">Número rodadas simuladas para convergência de resultados. Com 150k já convergem. </param>
        /// <returns>Probabilidade de ganhar. Ex: 50.10</returns>
        public static float GetRecalculaVitoria(Carta [] mao, Carta[] mesa, uint numeroRodadas = 150000) => AvaliaProbabilidadeMao.GetRecalculaVitoria(mao, mesa, null, numeroRodadas);

        /// <summary>
        /// Calcula a probabilidade de uma mão vencer.
        /// </summary>
        /// <param name="mao">Cartas da mão a ser avaliada. Pode ser 1 ou 2.</param>
        /// <param name="mesa">Cartas que já estão na mesa. De 0 a 5 cartas.</param>
        /// <param name="maoAdversaria">Cartas da mão adversária. Pode ser 0, 1 ou 2.</param>
        /// <param name="numeroRodadas">Número rodadas simuladas para convergência de resultados. Com 150k já convergem. </param>
        /// <returns>Probabilidade de ganhar. Ex: 50.10</returns>
        public static float GetRecalculaVitoria(Carta [] maoAvaliada, Carta[] mesa, Carta[] maoAdversaria, uint numeroRodadas = 100000)
        {
            uint vitorias = 0, derrotas = 0, empate = 0;

            AvaliaProbabilidadeMao avalia = new AvaliaProbabilidadeMao(maoAvaliada, maoAdversaria, mesa, numeroRodadas);
            avalia.Avalia(out vitorias, out derrotas, out empate);

            float _vitorias = vitorias, _derrotas = derrotas, _empates = empate, _numRod = numeroRodadas; 
            float probabilidadeFinal = ((_vitorias * 100) / _numRod);
            
            return probabilidadeFinal;

        }

    }
}
