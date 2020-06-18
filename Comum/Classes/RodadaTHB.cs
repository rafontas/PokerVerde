using Comum.AbstractClasses;
using Enuns;
using Modelo;

namespace Comum.Classes
{
    public class RodadaTHB : RodadaBase
    {
        public uint ValorApostadoRodadaJogador { get; set; }
        public uint ValorApostadoRodadaBanca { get; set; }

        // Ver como fazer um controle melhor de rodada. Se adiciono Lista de acao e a cada acao, da um push? Parece uma boa ideia
        // Lembrar de terminar de corrigir o bug do retorno dos valores no empate

        public RodadaTHB(TipoRodada tp, uint valorPotePreRodada, Carta[] cartaMesa) : base(cartaMesa, valorPotePreRodada, tp) { }
    }
}
