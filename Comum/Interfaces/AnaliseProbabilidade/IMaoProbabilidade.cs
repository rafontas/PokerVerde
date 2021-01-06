using Modelo;
using System.Collections.Generic;

namespace Comum.Interfaces.AnaliseProbabilidade
{
    public interface IMaoProbabilidade
    {
        int Id { get; }
        IDictionary<Carta, Carta> CartasMao { get; }

        void AddMesa(Carta c);

        string HandTokenizada { get; }
        string PocketHandTokenizada { get; }

        float ProbabilidadeVitoria { get; set; }

        string ToMaoTokenizada();
    }
}
