using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Excecoes
{
    public class DAOException : Exception
    {
        public DAOException() : base() { }
        public DAOException(string message) : base(message) { }
        public DAOException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}