using Comum.Interfaces;

namespace JogadorTH
{
    public class Corrida : ICorrida
    {
        public uint QtdPartidasJogadas { get; set; }

        public uint ValorInicial { get; set; }

        public uint ValorkAtual { get; set; }

        public uint ValorGanho { get; set; }

        public uint ValorPerdido { get; set; }

        public uint ValorDeParada { get => this.ValorInicial * 2; }
    }
}
