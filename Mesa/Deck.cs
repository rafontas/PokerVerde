using Enuns;
using MesaTh.Excecoes;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MesaTh
{
    public class Deck
    {
        private const int MAXIMO_CARTAS = 52;
        private IList<Carta> Baralho { get; set; }

        public Deck()
        {
            CreateDeck();
        }

        /// <summary>
        /// Cria um deck já embaralhado.s
        /// </summary>
        private void CreateDeck()
        {
            Baralho = new List<Carta>();
            for (uint i = 1; i <= 13; i++)
            {
                Baralho.Add(new Carta(i, Naipe.Copas));
                Baralho.Add(new Carta(i, Naipe.Ouros));
                Baralho.Add(new Carta(i, Naipe.Paus));
                Baralho.Add(new Carta(i, Naipe.Espadas));
            }

            Shuffle();
        }

        /// <summary>
        /// Zera o estado do baralho e faz outro completo e já embaralhado.
        /// </summary>
        public void Restart()
        {
            CreateDeck();
        }

        /// <summary>
        /// Embaralha o baralho dentro do deck.
        /// </summary>
        public void Shuffle()
        {
            Random rand = new Random();
            Carta aux;

            for (int i = 0; i < Baralho.Count; i++)
            {
                int novaPosicao = rand.Next(14);
                aux = Baralho[i];
                Baralho[i] = Baralho[novaPosicao];
                Baralho[novaPosicao] = aux;
            }
        }

        /// <summary>
        /// Verifica se o deck está válido
        /// </summary>
        public bool Valid()
        {
            return Baralho.Count == MAXIMO_CARTAS;
        }

        /// <summary>
        /// Remove o número de cartas desejadas do deck;
        /// </summary>
        /// <param name="cartasDescartadas">Número de cartas que serão retornadas, caso o deck acabe retorna o máximo possível.</param>
        /// <returns></returns>
        public IList<Carta> Pop(int cartasRetornadas)
        {
            if (cartasRetornadas < 0) throw new DeckException("Numero de cartas pedido não pode ser menor que zero.");
            IList<Carta> cartasPop = new List<Carta>();

            for (int i = 0; i < cartasRetornadas || Baralho.Count == 0; i++)
            {
                cartasPop.Add(Baralho[Baralho.Count - 1]);
                Baralho.RemoveAt(Baralho.Count - 1);
            }

            return cartasPop;
        }

        /// <summary>
        /// Remove uma de carta deck e a retorna.
        /// </summary>
        /// <returns>Uma carta</returns>
        public Carta Pop()
        {
            if (Baralho.Count <= 0) throw new DeckException("Não há cartas a serem distribuídas.");

            Carta c = Baralho.LastOrDefault();
            Baralho.Remove(Baralho.Last());
            return c;
        }

    }
}
