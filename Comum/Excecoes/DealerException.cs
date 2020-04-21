using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Excecoes
{
    public class DealerException : Exception
    {
        public DealerException() : base() { }
        public DealerException(string message) : base(message) { }
        public DealerException(string messagem, System.Exception inner) : base(messagem, inner) { }
    }
}
