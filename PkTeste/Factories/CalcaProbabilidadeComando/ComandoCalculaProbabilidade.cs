using System;
using System.Collections.Generic;
using System.Text;

namespace PkTeste.Factories.CalculadoraComando
{
    internal class ComandoCalculaProbabilidade : IComando
    {
        public string id => "c";

        public string desc { 
            get
            {
                StringBuilder strBuild = new StringBuilder("Calcula a probabilidade de determinada mão ganhar a partida.");
                strBuild.Append(Environment.NewLine);
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\tc --princ=\"<cartas-principais>\" --sec=\"<cartas-mão-adversária>\" --mes\"<cartas-mesa>\"");
                strBuild.Append(Environment.NewLine);
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\tArgumentos:");
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\t\t--princ = cartas da mão pricipal a ser avaliada. No mínimo uma carta. [Obrigatório]");
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\t\t--sec= cartas da mão secundária a ser avaliada contra a principal. [Opicional]");
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\t\t--mes= cartas já na mesa. [Opicional]");
                strBuild.Append(Environment.NewLine);
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\tIdentificando as cartas:");
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\t\tNaipes: \"co\" = copas, \"ou\" = ouros, \"es\" = espadas e \"pa\" = paus.");
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\t\tNumeros: 1 a 13.");
                strBuild.Append(Environment.NewLine);
                strBuild.Append("\t\tExemplos: \"1co 7pa\", \"5co 4co\", \"13es 11ou\"");
                strBuild.Append(Environment.NewLine);

                return strBuild.ToString();
            } 
        }

        public string[] args => throw new NotImplementedException();

        public string getMeusParametros(string comandosEArgumentosPassados)
        {
            throw new NotImplementedException();
        }

        public bool Validar()
        {
            throw new NotImplementedException();
        }

    }
}
