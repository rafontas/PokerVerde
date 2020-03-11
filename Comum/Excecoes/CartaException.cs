using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Excecoes
{
    public class CartaException : Exception
    {
        public CartaException() : base() { }
        public CartaException(string message) : base(message) { }
        public CartaException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}