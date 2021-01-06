using DealerTH.Probabilidade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Testes
{
    [TestClass]
    public class AvaliaMaoTest
    {
        [TestMethod]
        public void Teste1()
        {
            IList<Carta> MaoP = new List<Carta>() {
                new Carta(2, Enuns.Naipe.Ouros),
                new Carta(2, Enuns.Naipe.Copas)
            };

            Carta[] maoPrimaria = new Carta[]
            {
                new Carta(2, Enuns.Naipe.Ouros),
                new Carta(2, Enuns.Naipe.Copas)
            };

            IList<Carta> MaoS = new List<Carta>() {
                new Carta(2, Enuns.Naipe.Ouros),
                new Carta(2, Enuns.Naipe.Copas),
            };

            IList<Carta> Mesa = new List<Carta>() {
                new Carta(4, Enuns.Naipe.Paus),
                new Carta(5, Enuns.Naipe.Espadas),
                new Carta(11, Enuns.Naipe.Copas),
                new Carta(11, Enuns.Naipe.Ouros)
            };

            Carta[] mesa = new Carta[]
            {
                new Carta(4, Enuns.Naipe.Paus),
                new Carta(5, Enuns.Naipe.Espadas),
                new Carta(11, Enuns.Naipe.Copas),
                new Carta(11, Enuns.Naipe.Ouros)
            };


            uint numeroDeJogos = 120000;

            float prob = 0.0f;
            float prob_2 = 0.0f;

            var sw = Stopwatch.StartNew();
                AvaliaProbabilidadeMao teste = new AvaliaProbabilidadeMao(MaoP, null, Mesa, numeroDeJogos);
                teste.Avalia();
                prob_2 = teste.Probabilidade;
                long elapsedMilliseconds = sw.ElapsedMilliseconds;
            sw.Stop();

            sw = Stopwatch.StartNew();
                prob = AvaliaProbabilidadeMao.GetRecalculaVitoriaParalelo(maoPrimaria, mesa, null, numeroDeJogos, 5);
                long elapsedMilliseconds_2 = sw.ElapsedMilliseconds;
            sw.Stop();

            Console.WriteLine("Singular: " + elapsedMilliseconds + " " + teste.RetornaProbabilidade((int)numeroDeJogos, (int)teste.Vitorias));
            Console.WriteLine("Paralelo: " + elapsedMilliseconds_2 + " " + prob);

            Assert.AreEqual(numeroDeJogos, (teste.Vitorias + teste.Empates + teste.Derrotas));
        }
    }
}
