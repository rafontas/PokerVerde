using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Excecoes
{
    public class MaoExcpetion : Exception
    {
        public MaoExcpetion() : base() { }
        public MaoExcpetion(string message) : base(message) { }
        public MaoExcpetion(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}
