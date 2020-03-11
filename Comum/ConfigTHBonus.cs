using Enuns;

namespace Modelo
{
    public class ConfiguracaoTHBonus
    {
        public uint Ant { get; set; }
        public uint Flop { get; set; }
        public uint Turn { get; set; }
        public uint River { get; set; }
        public uint ValorMomento (TipoRodada tipo)
        {
            switch (tipo)
            {
                case TipoRodada.PreJogo: return this.Ant;
                case TipoRodada.Flop: return this.Flop;
                case TipoRodada.Turn: return this.Turn;
                case TipoRodada.River: return this.River;
                default: return 0;
            }
        }
    }
}
