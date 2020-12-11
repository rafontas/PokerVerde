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
                new Carta(12, Enuns.Naipe.Copas),
                new Carta(13, Enuns.Naipe.Copas),
            };

            Carta[] maoPrimaria = new Carta[]
            {
                new Carta(12, Enuns.Naipe.Copas),
                new Carta(13, Enuns.Naipe.Copas)
            };

            IList<Carta> MaoS = new List<Carta>() {
                new Carta(9, Enuns.Naipe.Ouros),
                new Carta(10, Enuns.Naipe.Copas),
            };

            uint numeroDeJogos = 1000000;
            float prob = 0.0f;

            var sw = Stopwatch.StartNew();
                AvaliaProbabilidadeMao teste = new AvaliaProbabilidadeMao(MaoP, null, null, numeroDeJogos);
                teste.Avalia();
                long elapsedMilliseconds = sw.ElapsedMilliseconds;
            sw.Stop();

            sw = Stopwatch.StartNew();
                AvaliaProbabilidadeMao.GetRecalculaVitoriaParalelo(maoPrimaria, null, null, numeroDeJogos, 5);
                long elapsedMilliseconds_2 = sw.ElapsedMilliseconds;
            sw.Stop();

            Console.WriteLine("Singular: " + elapsedMilliseconds + " " + teste.RetornaProbabilidade((int)numeroDeJogos, (int)teste.Vitorias));
            Console.WriteLine("Paralelo: " + elapsedMilliseconds_2 + " " + prob);

            Assert.AreEqual(numeroDeJogos, (teste.Vitorias + teste.Empates + teste.Derrotas));
        }
    }
}
