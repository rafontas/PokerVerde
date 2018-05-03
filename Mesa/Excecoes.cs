using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Excecoes
{
    public class CartaInvalidaException : Exception
    {
        public CartaInvalidaException() : base() { }
        public CartaInvalidaException(string message) : base(message) { }
        public CartaInvalidaException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }

    public class DeckException : Exception
    {
        public DeckException() : base() { }
        public DeckException(string message) : base(message) { }
        public DeckException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }

    public class JogadorExcpetion : Exception
    {
        public JogadorExcpetion() : base() { }
        public JogadorExcpetion(string message) : base(message) { }
        public JogadorExcpetion(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}
