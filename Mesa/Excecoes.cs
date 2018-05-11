using System;
using System.Collections.Generic;
using System.Text;

namespace MesaTh.Excecoes
{
    public class CartaInvalidaException : Exception
    {
        public CartaInvalidaException() : base() { }
        public CartaInvalidaException(string message) : base(message) { }
        public CartaInvalidaException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }

    public class JogadorExcpetion : Exception
    {
        public JogadorExcpetion() : base() { }
        public JogadorExcpetion(string message) : base(message) { }
        public JogadorExcpetion(string messagem, System.Exception inner) : base(messagem, inner) { }
    }

    public class MaoExcpetion : Exception
    {
        public MaoExcpetion() : base() { }
        public MaoExcpetion(string message) : base(message) { }
        public MaoExcpetion(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}
