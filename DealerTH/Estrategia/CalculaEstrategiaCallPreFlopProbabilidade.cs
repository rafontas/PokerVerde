using Comum.Classes.Poker;
using Comum.Interfaces;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaoTH.Estrategia
{
    public class CalculaEstrategiaCallPreFlopProbabilidade : IEstrategiaDescricao
    {
        public string DescricaoGeral => "Considera apenas probabilidade de vencer preFlop.";

        public string PreFlop => "Considera apenas probabilidade de vencer preFlop.";

        public string Flop => "Apenas Call em qualquer situação.";

        public string Turn => "Apenas Call em qualquer situação.";

        public string River => "Apenas Call em qualquer situação.";

        public string CondicaoDeParada => "Acabarem as fichas";

        private int QuantidadeJogosAhSimular { get; set; }
        
        private int StackInicial { get; set; }
        
        private int StackFinal { get; set; }

        private Carta [] Cartas { get; set; }

        public CalculaEstrategiaCallPreFlopProbabilidade(Carta c1, Carta c2, int stackInicial, int quantidadeJogosSimular)
        {
            this.Cartas = new Carta[2] { c1, c2 };
            this.StackInicial = stackInicial;
            this.QuantidadeJogosAhSimular = quantidadeJogosSimular;
        }

        public CalculaEstrategiaCallPreFlopProbabilidade Get(Carta c1, Carta c2)
        {
            throw new NotImplementedException();
        }
        
        public void Calcula(IJogador jogador, IJogador banca) 
        {
            ISitAndGo SitAndGo =
                new BuilderSitAndGo(new ConfiguracaoTHBonus())
                .addJogador(jogador)
                .SetBanca(banca)
                .ToSitAndGo();

            SitAndGo.Executa();
        }
        
        public void Persiste() { }

        public bool Exists(Carta c1, Carta c2) 
        { 
            throw new NotImplementedException();
        }
    }
}
