using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace MesaTH
{
    public class InfoMesa
    {
        public static List<InfoMesa> InfoRodadas { get; set; } = new List<InfoMesa>();

        public MomentoJogo Momento { get; set; } = MomentoJogo.PreJogo;
        public TipoJogadorTHB JogadorGanhador { get; set; } = TipoJogadorTHB.SemJogador;
        public IJogador Jogador { get; set; }

        public Carta[] Flop { get; set; } = new Carta[] { null, null, null };
        public Carta Turn = null;
        public Carta River = null;

        public Carta[] CartasJogador = new Carta[] { null, null };
        public Carta[] CartasBanca = new Carta[] { null, null };

        public uint GanhosMesa { get; set; } = 0;

        public uint ValorAnt { get; set; }
        public uint ValorFlop { get; set; }
        public uint ValorTurn { get; set; }
        public uint ValorRiver { get; set; }

        public uint ValorInvestidoAnt { get; set; }
        public uint ValorInvestidoFlop { get; set; } = 0;
        public uint ValorInvestidoTurn { get; set; } = 0;
        public uint ValorInvestidoRiver { get; set; } = 0;
        public uint ValorInvestido { get
            {
                return ValorInvestidoAnt + ValorInvestidoFlop + ValorInvestidoRiver + ValorInvestidoTurn;
            }
        }

        public int NumMaximoRodadas { get; set; } = 1;
        public int NumRodada { get; set; } = 1;

        /// <summary>
        /// Reinicia as informações pertinentes a uma rodada.
        /// </summary>
        public void ReiniciaInfoMesa(TipoJogadorTHB jogadorGanhador)
        {
            JogadorGanhador = jogadorGanhador;
            InfoRodadas.Add(CloneMesa());

            Flop = new Carta[] { null, null, null };
            Turn = null;
            River = null;
            CartasJogador = new Carta[] { null, null };
            CartasBanca = new Carta[] { null, null };

            ValorInvestidoFlop = 0;
            ValorInvestidoTurn = 0;
            ValorInvestidoRiver = 0;
            NumRodada++;
            Momento = MomentoJogo.PreFlop;
            JogadorGanhador = TipoJogadorTHB.SemJogador;
        }

        /// <summary>
        /// Retorna todo o valor investido na mesa.
        /// </summary>
        /// <returns>Valor investido</returns>
        public uint GetPote() => 2 * (ValorInvestidoFlop + ValorInvestidoTurn + ValorInvestidoRiver);

        /// <summary>
        /// Retorna as informações da mesa impressas;
        /// </summary>
        /// <returns>String formatada as infos da mesa.</returns>
        public override string ToString()
        {
            string conteudo = "";

            conteudo += "Informação da mesa: " + Environment.NewLine;
            conteudo += "Rodada: " + NumRodada + "/" + NumMaximoRodadas + Environment.NewLine;
            conteudo += "Momento Jogo: " + Momento.ToString() + Environment.NewLine;
            conteudo += "Flop: " + PrintFlop() + Environment.NewLine;
            conteudo += "Turn: " + PrintTurn() + Environment.NewLine;
            conteudo += "River: " + PrintRiver() + Environment.NewLine;
            conteudo += "Cartas Banca: " + PrintCartasBanca() + Environment.NewLine;
            conteudo += "Jogador (" + Jogador.GetStack() + ") " + PrintCartasJogador() + Environment.NewLine;
            conteudo += "Valores investidos: " + "[" + ValorInvestidoAnt + " " + ValorInvestidoFlop + " " + ValorInvestidoTurn + " " + ValorInvestidoRiver + "]" + Environment.NewLine;

            return conteudo;
        }

        /// <summary>
        /// Retorna os valores máximo possíves de serem apostados;
        /// </summary>
        /// <returns>Valores máximos possíveis de serem apostados imprimíveis.</returns>
        public string ValoresMaximosAposta()
            => PrintValorAnt() + ", " +
                PrintValorFlop() + ", " +
                PrintValorTurn() + ", " +
                PrintValorRiver() + Environment.NewLine;

        /// <summary>
        /// Retorna os valores apostados pelo jogador;
        /// </summary>
        /// <returns>Valores apostados imprimíveis</returns>
        public string ValoresApostados()
            => PrintValorInvestidoAnt() + ", " +
                PrintValorInvestidoFlop() + ", " +
                PrintValorInvestidoTurn() + ", " +
                PrintValorInvestidoRiver() + Environment.NewLine;

        private string PrintFlop()
        {
            if (Flop[0] == null) return "";
            return Flop[0] + " " + Flop[1] + " " + Flop[2];
        }
        private string PrintTurn() => Turn == null ? "" : Turn.ToString();
        private string PrintRiver() => River == null ? "" : River.ToString();
        private string PrintCartasJogador()
        {
            if (CartasJogador == null || Momento == MomentoJogo.PreJogo) return "";
            return (CartasJogador[0]?.ToString()??" ") + " " + (CartasJogador[1]?.ToString()??" ");
        }
        private string PrintCartasBanca()
        {
            if (CartasBanca == null || CartasBanca[0] == null) return "";
            return CartasBanca[0].ToString() + " " + CartasBanca[1].ToString();
        }

        private string PrintValorAnt() => "Máximo Ant: " + ValorAnt.ToString();
        private string PrintValorFlop() => "Máximo Flop: " + ValorFlop.ToString();
        private string PrintValorTurn() => "Máximo Turn: " + ValorTurn.ToString();
        private string PrintValorRiver() => "Máximo River: " + ValorRiver.ToString();

        private string PrintValorInvestidoAnt() => "Ant: " + ValorInvestidoAnt.ToString();
        private string PrintValorInvestidoFlop() => "Flop: " + ValorInvestidoFlop.ToString();
        private string PrintValorInvestidoTurn() => "Turn: " + ValorInvestidoTurn.ToString();
        private string PrintValorInvestidoRiver() => "River: " + ValorInvestidoRiver.ToString();

        /// <summary>
        /// Clona o objeto InfoMesa criando nova referência para todos objetos
        /// </summary>
        /// <returns>Novo objeto</returns>
        public InfoMesa CloneMesa()
        {
            return new InfoMesa()
            {
                Flop = new Carta[] {
                    this.Flop[0].Clone(),
                    this.Flop[1].Clone(),
                    this.Flop[2].Clone()
                },
                River = this.River.Clone(),
                Turn = this.Turn.Clone(),
                CartasJogador = new Carta[] {
                    this.CartasJogador[0].Clone(),
                    this.CartasJogador[1].Clone()
                },
                CartasBanca = new Carta []
                {
                    this.CartasBanca[0].Clone(),
                    this.CartasBanca[1].Clone(),
                },
                ValorAnt = this.ValorAnt,
                ValorFlop = this.ValorFlop,
                ValorTurn = this.ValorTurn,
                ValorRiver = this.ValorRiver,
                ValorInvestidoFlop = this.ValorInvestidoFlop,
                ValorInvestidoTurn = this.ValorInvestidoTurn,
                ValorInvestidoRiver = this.ValorInvestidoRiver,
                NumRodada = this.NumRodada,
                JogadorGanhador = this.JogadorGanhador
            };
        }
    }
}
