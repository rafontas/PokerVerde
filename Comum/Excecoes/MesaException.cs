using System;

namespace Modelo
{
    public class MesaException : Exception
    {
        public MesaException() : base() { }
        public MesaException(string message) : base(message) { }
        public MesaException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}
