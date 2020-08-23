using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testes.ComumTeste
{
    [TestClass]
    public class CartaTest
    {
        [TestMethod]
        public void ToJsonTest() 
        {
            Carta carta = new Carta(9, Enuns.Naipe.Copas);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.AppendFormat("\t\"Numero\":\"9\"");
            stringBuilder.Append("," + Environment.NewLine);
            stringBuilder.AppendFormat("\t\"Naipe\":\"♥\"");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");

            Assert.IsTrue(stringBuilder.ToString() == carta.ToJson());

            Carta carta2 = new Carta(11, Enuns.Naipe.Ouros);

            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("{");
            stringBuilder2.Append(Environment.NewLine);
            stringBuilder2.AppendFormat("\t\"Numero\":\"11\"");
            stringBuilder2.Append("," + Environment.NewLine);
            stringBuilder2.AppendFormat("\t\"Naipe\":\"♦\"");
            stringBuilder2.Append(Environment.NewLine);
            stringBuilder2.Append("}");

            Assert.IsTrue(stringBuilder2.ToString() == carta2.ToJson());
        }

        [TestMethod]
        public void FromJsonTest()
        {
            Carta carta = new Carta(9, Enuns.Naipe.Copas);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.AppendFormat("\t\"Numero\":\"9\"");
            stringBuilder.Append("," + Environment.NewLine);
            stringBuilder.AppendFormat("\t\"Naipe\":\"♥\"");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");

            Assert.IsTrue(carta.Equals(Carta.FromJson(stringBuilder.ToString())));

            Carta carta2 = new Carta(11, Enuns.Naipe.Ouros);

            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.Append("{");
            stringBuilder2.Append(Environment.NewLine);
            stringBuilder2.AppendFormat("\t\"Numero\":\"11\"");
            stringBuilder2.Append("," + Environment.NewLine);
            stringBuilder2.AppendFormat("\t\"Naipe\":\"♦\"");
            stringBuilder2.Append(Environment.NewLine);
            stringBuilder2.Append("}");

            Assert.IsTrue(carta2.Equals(Carta.FromJson(stringBuilder2.ToString())));
        }

        [TestMethod]
        public void FromStringTest()
        {
            Carta carta = new Carta(9, Enuns.Naipe.Copas);

            Assert.IsTrue(carta.Equals(Carta.FromString("9_♥")));

            carta = new Carta(12, Enuns.Naipe.Copas);
            Assert.IsTrue(carta.Equals(Carta.FromString("12_♥")));

            carta = new Carta(14, Enuns.Naipe.Ouros);
            Assert.IsTrue(carta.Equals(Carta.FromString("14_♦")));
        }
    }
}
