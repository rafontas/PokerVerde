using Comum.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Classes.Poker
{
    public class CroupierConstructParam
    {        
        public IJogador Jogador { get; set; }
        public Carta [] CartasJogador { get; set; }

        public IJogador Banca { get; set; }
        public Carta [] CartasBanca { get; set; }

        public IConfiguracaoPoker ConfiguracaoPoker { get; set; }
    }
}
