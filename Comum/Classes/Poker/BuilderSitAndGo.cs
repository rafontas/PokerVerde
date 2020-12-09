using System.Collections.Generic;
using System.Linq;
using Modelo;
using Comum.Interfaces;
using Comum.Interfaces.PokerBase;

namespace Comum.Classes.Poker
{
    public class BuilderSitAndGo : IBuilderSitAndGo
    {
        private IConfiguracaoPoker ConfiguracaoPoker { get; set; }

        private IList<IJogador> Jogadores { get; set; }

        private IJogador Banca { get; set; }

        public BuilderSitAndGo(IConfiguracaoPoker ConfiguracaoPoker)
        {
            this.ConfiguracaoPoker = ConfiguracaoPoker;
            this.Jogadores = new List<IJogador>();
        }

        public IBuilderSitAndGo addJogador(uint quantidadePartidasExecutar, IJogador jogador)
        {
            IJogador Jogador = jogador;
            Jogador.Corrida = new Corrida(quantidadePartidasExecutar);

            this.Jogadores.Add(Jogador);
            return this;
        }

        public IBuilderSitAndGo addJogador(IJogador jogador)
        {
            this.Jogadores.Add(jogador);
            return this;
        }

        public ISitAndGo ToSitAndGo()
        {
            IJogador jogador = this.Jogadores.First();

            return new SitAndGo(this.ConfiguracaoPoker, this.Banca, jogador);
        }

        public IBuilderSitAndGo SetBanca(IJogador jogador)
        {
            // Dealer Mesa
            if (this.Banca == null) this.Banca = jogador;

            return this;
        }
    }
}
