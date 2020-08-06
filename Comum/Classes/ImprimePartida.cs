using Comum.Interfaces;
using Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comum.Classes
{
    public class ImprimePartida : IImprimePartida
    {
        uint valorTotalGanho { get; set; } = 0;
        uint valorTotalPerdido { get; set; } = 0;

        public string pequenoResumo(IList<IPartida> partidas)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            IJogadorStack jogStack = partidas?.First().Jogador?.JogadorStack;
            if (jogStack == null) return "";

            int vitorias = 0, derrotas = 0, empates = 0;

            vitorias = partidas.Where(p => p.JogadorGanhador == VencedorPartida.Jogador).Count();
            derrotas = partidas.Where(p => p.JogadorGanhador == VencedorPartida.Banca).Count();
            empates = partidas.Where(p => p.JogadorGanhador == VencedorPartida.Empate).Count();

            stringBuilder.AppendFormat("Resumo dos jogos" + Environment.NewLine);
            stringBuilder.AppendFormat("V\tD\tE" + Environment.NewLine);
            stringBuilder.AppendFormat("{0}\t{1}\t{2}" + Environment.NewLine + Environment.NewLine, vitorias, derrotas, empates);
            
            stringBuilder.AppendFormat("Resumo do stack " + Environment.NewLine);
            stringBuilder.AppendFormat("Inicial: {0}" + Environment.NewLine, jogStack.StackInicial);
            stringBuilder.AppendFormat("Final: {0}" + Environment.NewLine, jogStack.Stack);

            return stringBuilder.ToString();
        }

        public string pequenoResumo(IPartida partida)
        {
            string seq, ganhador, valorGanho;
            
            StringBuilder stringBuilder = new StringBuilder();

            seq = partida.SequencialPartida.ToString();
            ganhador = partida.JogadorGanhador.ToString();
            valorGanho = this.GetValorGanho(partida);

            stringBuilder.AppendFormat("{0}\t\t{1}\t\t{2}", seq, ganhador, valorGanho);

            return stringBuilder.ToString();
        }

        public string pequenoResumoTodasPartidas(IList<IPartida> partidas)
        {
            this.valorTotalGanho = 0;
            this.valorTotalPerdido = 0;

            StringBuilder stringBuilder = new StringBuilder("#\t\tGanhador\tValorGanho" + Environment.NewLine);

            foreach (IPartida p in partidas)
                stringBuilder.AppendLine(this.pequenoResumo(p));

            IJogadorStack jogStack = partidas?.First().Jogador?.JogadorStack;
            if (jogStack == null) return "";

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("Ganhos: {0}" + Environment.NewLine, this.valorTotalGanho);
            stringBuilder.AppendFormat("Perdas: {0}" + Environment.NewLine, this.valorTotalPerdido);

            return stringBuilder.ToString();
        }

        private string GetValorGanho(IPartida p)
        {
            string valor;
            
            switch(p.JogadorGanhador)
            {
                case VencedorPartida.Banca:
                    valor = "-" + p.ValorInvestidoJogador;
                    this.valorTotalPerdido += p.ValorInvestidoJogador;
                    break;

                case VencedorPartida.Jogador:
                    valor = "" + p.ValorInvestidoBanca;
                    this.valorTotalGanho += p.ValorInvestidoBanca;
                    break;

                case VencedorPartida.Empate:
                    valor = "0";
                    break;

                default:
                    throw new Exception("Jogador ganhador não reconhecido.");

            }

            return valor;
        }
    }
}
