using System;
using System.Collections.Generic;
using System.Text;
using PkTeste.Interfaces;

namespace PkTeste
{
    public class Comando
    {
        public string id { get; private set; }
        public string desc { get; private set; }
        public string[] args { get; private set; }

        public Comando(string id, string desc, string [] args = null) {
            this.desc = desc;
            this.id = id;
            this.args = args;
        }

        public string getDesc() => (this.id + ": " + this.desc);
    }

}
