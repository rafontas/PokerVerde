using Microsoft.VisualStudio.TestTools.UnitTesting;
using PkTeste;
using PkTeste.Interfaces;
using System.Collections.Generic;

namespace PkUnitTestes
{
    [TestClass]
    public class TestaComandos
    {
        [TestMethod]
        public void TestaComandosBasicos()
        {
            IPokerComandos teste = new ComandosBasicos();
            IDictionary<string, Comando> comandos = teste.getComandos();
            
            foreach (var c in comandos) 
                Assert.IsTrue(teste.validaComando(c.Value));
        }

        [TestMethod]
        public void TestaComandosNaAnalise()
        {
            IPokerComandos teste = new ComandosNaAnalise();
            IDictionary<string,Comando> comandos = teste.getComandos();

            foreach (var c in comandos)
                Assert.IsTrue(teste.validaComando(c.Value));

        }
    }
}
