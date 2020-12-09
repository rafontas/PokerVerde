using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface IMaoBasica : IClone<IMaoBasica>, IEquatable<IMaoBasica>
    {
        uint NumCarta1 { get; set; }
        uint NumCarta2 { get; set; }
        char OffOrSuited { get; set; }
    }
}
