using Comum.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes
{
    public class SitAndGo : ISitAndGo
    {
        public IConfiguracaoPoker ConfiguracaoPoker { get ; private set; }

        public IList<IJogador> Jogadores { get; set; }

        private ICroupier Croupier { get; set; }

        private Mesa Mesa { get; set; }
        
        /// <summary>
        /// Executa todas as corridas
        /// </summary>
        public void Executa()
        {
            this.Croupier.ExecutaTodasCorridas();
        }

        /// <summary>
        /// Inicia um SitAndGo.
        /// </summary>
        /// <param name="configuracaoPoker">Valors da configuração</param>
        /// <param name="Banca">O Jogador que representa a Banca.</param>
        public SitAndGo(IConfiguracaoPoker configuracaoPoker, IJogador Banca, IJogador Jogador)
        {
            //todo: retirar a banca do parâmetro, para ficar desacoplado
            this.ConfiguracaoPoker = configuracaoPoker;
            this.Mesa = new Mesa(this.ConfiguracaoPoker);
            this.Jogadores = new List<IJogador>() { Jogador };
            this.Croupier = new Croupier(this.Mesa, Banca, Jogador);
        }
    }
}
