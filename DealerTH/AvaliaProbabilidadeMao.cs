using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

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

            for (uint i = 1; i <= NumeroRodadas; i++)
            {
                // Preenche as cartas faltantesk
                for (int j = NumCartasMesa, k = NumCartasMao, l = NumCartasMaoSec; j < limite; j++, k++, l++)
                {
                    CartaA aux;
                    // Preenhce a mao
                    if (k < LIMITE_MAO_P)
                    {
                        aux = Deck.CartaRandom;
                        while (aux.RodadaNum == i) aux = Deck.CartaRandom;
                        MaoPrincipal[k] = aux;
                        aux.RodadaNum = i;
                    }

                    // Preenche mão secundária
                    if (l < LIMITE_MAO_S)
                    {
                        aux = Deck.CartaRandom;
                        while (aux.RodadaNum == i) aux = Deck.CartaRandom;
                        MaoSecundaria[l] = aux;
                        aux.RodadaNum = i;
                    }

                    // Preenhce a mesa
                    if (j < LIMITE_MESA)
                    {
                        aux = Deck.CartaRandom;
                        while (aux.RodadaNum == i) aux = Deck.CartaRandom;
                        Mesa[j] = aux;
                        aux.RodadaNum = i;
                    }
                }

                MelhorMao principal = new MelhorMao();
                MelhorMao secundaria = new MelhorMao();

                // Avalia
                MaoTexasHoldem mp = principal.AvaliaMao(new List<Carta>() {
                    MaoPrincipal[0], MaoPrincipal[1],
                    Mesa[0], Mesa[1], Mesa[2],
                    Mesa[3], Mesa[4],
                });
                MaoTexasHoldem ms = secundaria.AvaliaMao(new List<Carta>() {
                    MaoSecundaria[0], MaoSecundaria[1],
                    Mesa[0], Mesa[1], Mesa[2],
                    Mesa[3], Mesa[4],
                });

                int resultado = mp.Compara(ms);

                if (resultado == 1) vitorias++;
                else if (resultado == -1) derrotas++;
                else empate++;
            }
        }

        /// <summary>
        /// Calcula a probabilidade de uma mão vencer.
        /// </summary>
        /// <param name="mao">Cartas da mão a ser avaliada. Podem ser passadas 1 ou 2 cartas.</param>
        /// <param name="numeroRodadas">Número rodadas simuladas para convergência de resultados. Com 150k já convergem. </param>
        /// <returns>Probabilidade de ganhar. Ex: 50.10</returns>
        public static float GetPorcentagemVitoria(Carta[] mao, uint numeroRodadas = 150000) => AvaliaProbabilidadeMao.GetPorcentagemVitoria(mao, null, null, numeroRodadas);

        /// <summary>
        /// Calcula a probabilidade de uma mão vencer.
        /// </summary>
        /// <param name="mao">Cartas da mão a ser avaliada. Pode ser 1 ou 2.</param>
        /// <param name="mesa">Cartas que já estão na mesa. De 0 a 5 cartas.</param>
        /// <param name="numeroRodadas">Número rodadas simuladas para convergência de resultados. Com 150k já convergem. </param>
        /// <returns>Probabilidade de ganhar. Ex: 50.10</returns>
        public static float GetPorcentagemVitoria(Carta [] mao, Carta[] mesa, uint numeroRodadas = 150000) => AvaliaProbabilidadeMao.GetPorcentagemVitoria(mao, mesa, null, numeroRodadas);

        /// <summary>
        /// Calcula a probabilidade de uma mão vencer.
        /// </summary>
        /// <param name="mao">Cartas da mão a ser avaliada. Pode ser 1 ou 2.</param>
        /// <param name="mesa">Cartas que já estão na mesa. De 0 a 5 cartas.</param>
        /// <param name="maoAdversaria">Cartas da mão adversária. Pode ser 0, 1 ou 2.</param>
        /// <param name="numeroRodadas">Número rodadas simuladas para convergência de resultados. Com 150k já convergem. </param>
        /// <returns>Probabilidade de ganhar. Ex: 50.10</returns>
        public static float GetPorcentagemVitoria(Carta [] maoAvaliada, Carta[] mesa, Carta[] maoAdversaria, uint numeroRodadas = 100000)
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
