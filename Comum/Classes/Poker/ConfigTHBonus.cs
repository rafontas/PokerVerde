using Comum.Interfaces;
using Enuns;

namespace Modelo
{
    public class ConfiguracaoTHBonus : IConfiguracaoPoker
    {
        public uint Ant { get; set; } = 5;
        public uint Flop { get; set; } = 10;
        public uint Turn { get; set; } = 5;
        public uint River { get; set; } = 5;
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

        public static IConfiguracaoPoker getConfiguracaoPadrao() => new ConfiguracaoTHBonus();
    }
}
