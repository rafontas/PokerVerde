using Comum.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace JogadorTH.Interfaces
{
    public class JogadorEstatisticas : IJogadorEstatisticas
    {
        public JogadorEstatisticas(IList<IPartida> historico)
        {
            this.Historico = historico;
        }

        private IList<IPartida> Historico { get; set; }

        public int getQuantidadeJogosGanhos() 
            => this.Historico.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Jogador).Count();

        public int getQuantidadeJogosPerdidos()
            => this.Historico.Where(p => p.JogadorGanhador == Enuns.VencedorPartida.Banca).Count();

        public float getValorGanhoPorJogo()
        {
            int saldoStackFinal = (int) (this.Historico.Last().Jogador.Stack - this.Historico.First().Jogador.StackInicial);
            int qtdJogos = this.Historico.Count;

            return saldoStackFinal / qtdJogos;
        }

        public int getQuantidadeJogosJogados() => (int)this.Historico.Count;

        public int getStackInicial() => (int) this.Historico.First().Jogador.StackInicial;
        public int getStackFinal() => (int) this.Historico.First().Jogador.Stack;

        public int getStackSaldoFinal() => (this.getStackFinal() - this.getStackInicial());

    }
}
