using Comum.Interfaces;
using Modelo;
using PkTeste.Interfaces;
using System.Collections.Generic;
using JogadorTH;
using JogadorTH.Inteligencia;
using Comum.Classes;
using System.Linq;

namespace PkTeste.Classes
{
    public class BuilderSitAndGo : IBuilderSitAndGo
    {
        private IConfiguracaoPoker ConfiguracaoPoker { get; set; }

        private IList<IJogador> Jogadores { get; set; }

        private IJogador Banca { get; set; }

        public BuilderSitAndGo(IConfiguracaoPoker ConfiguracaoPoker) {
            this.ConfiguracaoPoker = ConfiguracaoPoker;
            this.Jogadores = new List<IJogador>();
        }

        public IBuilderSitAndGo addJogador(uint quantidadePartidasExecutar)
        {
            IJogador Jogador = new DummyJogadorTHB(this.ConfiguracaoPoker, 10000, new DummyInteligencia());
            Jogador.Corrida = new Corrida(quantidadePartidasExecutar);

            this.Jogadores.Add(Jogador);
            return this;
        }

        //public IBuilderSitAndGo addJogador()
        //{
        //    this.Jogadores.Add(new DummyJogadorTHB(this.ConfiguracaoPoker, 10000, new DummyInteligencia()));
        //    return this;
        //}

        public IBuilderSitAndGo addJogador(IJogador jogador)
        {
            this.Jogadores.Add(jogador);
            return this;
        }

        public IBuilderSitAndGo SetRestantePadrao()
        {
            // Dealer Mesa
            if (this.Banca == null) 
                this.Banca = new Banca(this.ConfiguracaoPoker);

            // todo: adicionar jogador builder e complementar este builder

            // Adiciona jogador de teste
            //if (this.Jogadores.Count == 0)this.addJogador();

            return this;
        }

        public ISitAndGo GetResult() 
        {
            if (this.Banca == null) this.SetRestantePadrao();
            IJogador jogador = this.Jogadores.First();

            return new SitAndGo(this.ConfiguracaoPoker, this.Banca, jogador);
        }

    }
}
