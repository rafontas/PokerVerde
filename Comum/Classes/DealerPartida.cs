using Comum.Interfaces;
using Enuns;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comum.Classes
{
    public class DealerPartida : IDealerPartida
    {
        public Mesa Mesa { get; }

        public DealerPartida(Mesa m) => this.Mesa = m;

        public bool HaJogadoresParaJogar() 
            => this.Mesa.Participantes.Count > 0;

        public void PrepararNovaPartida() => this.Mesa.ReiniciarMesa();

        public void PergutarQuemIraJogar()
        {
            foreach (IJogador j in this.Mesa.Participantes)
            {
                IAcaoTomada a = j.PreJogo(this.Mesa.RegrasMesaAtual.Ant);

                if (a.Acao == AcoesDecisaoJogador.Play) {
                    this.Mesa.PartidasAtuais.Add(j, new Partida(j.SeqProximaPartida));
                }
                else if (a.Acao == AcoesDecisaoJogador.Stop)
                {
                    this.EncerrarPartidaJogador(j);
                }
            }
        }

        public void CobrarAnt()
        {
            foreach(IJogador j in this.Mesa.ParticipantesJogando)
                this.Mesa.PartidasAtuais[j].AddToPote(j.PagaValor(this.Mesa.RegrasMesaAtual.Ant));
        }

        public void DistribuirCartasJogadores()
        {
            foreach(IJogador j in this.Mesa.ParticipantesJogando)
                j.RecebeCarta(this.Mesa.Deck.Pop(), this.Mesa.Deck.Pop());
        }

        public void RevelarFlop()
        {
            this.Mesa.CartasMesa = new Carta[]
            {
                this.Mesa.Deck.Pop(),
                this.Mesa.Deck.Pop(),
                this.Mesa.Deck.Pop()
            };
        }

        public void RevelarTurn() => this.Mesa.River = this.Mesa.Deck.Pop();

        public void RevelarRiver() => this.Mesa.River = this.Mesa.Deck.Pop();

        public void PerguntarPagarFlop()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                IAcaoTomada a = jog.Key.PreFlop(this.Mesa.RegrasMesaAtual.Flop);

                if (a.Acao == AcoesDecisaoJogador.PayFlop)
                {
                    jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Flop);

                    jog.Value.AddToPote(this.Mesa.RegrasMesaAtual.Flop);
                    jog.Value.AddToPote(this.Mesa.RegrasMesaAtual.Flop);
                }
                else if (a.Acao == AcoesDecisaoJogador.Fold)
                {
                    this.EncerrarPartidaJogador(jog.Key);
                }
            }
        }

        public void PerguntarAumentarPreTurn()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                IAcaoTomada a = jog.Key.Flop(this.Mesa.CartasMesa, this.Mesa.RegrasMesaAtual.Flop);

                if (a.Acao == AcoesDecisaoJogador.Raise)
                {
                    uint valorAddPote = jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn) * 2;
                    jog.Value.AddToPote(valorAddPote);
                }
                else if (a.Acao == AcoesDecisaoJogador.Fold)
                {
                    this.EncerrarPartidaJogador(jog.Key);
                }
            }
        }

        public void PerguntarAumentarPreRiver()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                IAcaoTomada a = jog.Key.Turn(this.Mesa.CartasMesa, this.Mesa.RegrasMesaAtual.Turn);

                if (a.Acao == AcoesDecisaoJogador.Raise)
                {
                    //Pega valor do jogador e da mes
                    uint valorAddPote = jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.River) * 2;
                    jog.Value.AddToPote(valorAddPote);
                }
                else if (a.Acao == AcoesDecisaoJogador.Fold)
                {
                    this.EncerrarPartidaJogador(jog.Key);
                }
            }

        }

        public void VerificarGanhadorPartida(IPartida p)
        {
            Carta [] CartasBanca = new Carta[] {
                p.Banca.Cartas[0],
                p.Banca.Cartas[1]
            };
            Carta [] CartasJogador = new Carta[] {
                p.Jogador.Cartas[0],
                p.Jogador.Cartas[1]
            };
            Carta[] CartasMesa = this.Mesa.CartasMesa;

            MaoTexasHoldem maoBanca = new MaoTexasHoldem() {
                Cartas = CartasMesa.Union(CartasBanca).ToList()
            };
            MaoTexasHoldem maoJogador = new MaoTexasHoldem() {
                Cartas = CartasMesa.Union(CartasJogador).ToList()
            };

            switch(maoJogador.Compara(maoBanca))
            {
                case -1: p.JogadorGanhador = VencedorPartida.Banca; 
                    break;

                case 0: p.JogadorGanhador = VencedorPartida.Empate; 
                    break;

                case 1: p.JogadorGanhador = VencedorPartida.Jogador;
                    p.Jogador.RecebeValor(p.PoteAgora);
                    break;
            }
        }

        public bool ExistePartidaEmAndamento() => this.Mesa.PartidasAtuais.Count > 0;

        public void EncerrarPartidas()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                this.VerificarGanhadorPartida(jog.Value);
                this.EncerrarPartidaJogador(jog.Key);
            }
        }

        public void EncerrarPartidaJogador(IJogador j)
        {
            j.AddPartidaHistorico(this.Mesa.PartidasAtuais[j]);
            this.Mesa.Participantes.Remove(j);
        }
    }
}
