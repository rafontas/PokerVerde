using System;

namespace Modelo
{
    public class DeckException : Exception
    {
        public DeckException() : base() { }
        public DeckException(string message) : base(message) { }
        public DeckException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}
