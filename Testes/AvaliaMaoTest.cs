using DealerTH.Probabilidade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System.Collections.Generic;

namespace Testes
{
    [TestClass]
    public class AvaliaMaoTest
    {
        [TestMethod]
        public void Teste1()
        {
            IList<Carta> MaoP = new List<Carta>() {
                new Carta(14, Enuns.Naipe.Espadas),
                new Carta(14, Enuns.Naipe.Copas),
            };

            IList<Carta> MaoS = new List<Carta>() {
                new Carta(9, Enuns.Naipe.Ouros),
                new Carta(10, Enuns.Naipe.Copas),
            };

            AvaliaProbabilidadeMao teste = new AvaliaProbabilidadeMao(MaoP, null, null);
            teste.Avalia(out uint vitoria, out uint derrotas, out uint empate);

            Assert.AreEqual((uint)100000, (vitoria + empate + derrotas));
        }
    }
}
