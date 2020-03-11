using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Excecoes
{
    public class JogadorException : Exception
    { 
        public JogadorException() : base() { }
        public JogadorException(string message) : base(message) { }
        public JogadorException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}
