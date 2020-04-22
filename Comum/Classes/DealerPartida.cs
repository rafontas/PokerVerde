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
        private IJogador banca { get; set; }

        public Mesa Mesa { get; }

        public DealerPartida(Mesa m, IJogador banca)
        { 
            this.Mesa = m;
            this.banca = banca;            
        }

        public bool HaJogadoresParaJogar() 
            => this.Mesa.Participantes.Count > 0;

        public void PrepararNovaPartida() 
            => this.Mesa.ReiniciarMesa();

        public void PergutarQuemIraJogar()
        {
            foreach (IJogador j in this.Mesa.Participantes)
            {
                IAcaoTomada a = j.PreJogo(this.Mesa.RegrasMesaAtual.Ant);

                if (a.Acao == AcoesDecisaoJogador.Play) {
                    this.Mesa.PartidasAtuais.Add(j, new Partida(j.SeqProximaPartida, j, this.banca));
                }
                else if (a.Acao == AcoesDecisaoJogador.Stop)
                {
                    this.EncerrarPartidaJogador(j);
                }
            }
        }

        public void ExecutarPreFlop()
        {
            foreach(var jogadorPartida in this.Mesa.PartidasAtuais)
            {
                this.CobrarAnt(jogadorPartida.Key);
                this.DistribuirCartasJogadores(jogadorPartida.Value);
                jogadorPartida.Value.AddRodada(new RodadaTHB(TipoRodada.PreFlop, 0, null));
            }
        }

        //todo: tirar isso de public
        public void CobrarAnt(IJogador jogador) =>
                this.Mesa.PartidasAtuais[jogador].AddToPote(jogador.PagaValor(this.Mesa.RegrasMesaAtual.Ant));


        //todo: tirar isso de public
        public void DistribuirCartasJogadores(IPartida p) => p.Jogador.RecebeCarta(p.PopDeck(), p.PopDeck());

        public void RevelarFlop() {
            foreach (var partida in this.Mesa.PartidasAtuais)
                partida.Value.RevelarFlop();
        }

        public void RevelarTurn() {
            foreach (var partida in this.Mesa.PartidasAtuais)
                partida.Value.RevelarTurn();
        }

        public void RevelarRiver() {
            foreach (var partida in this.Mesa.PartidasAtuais)
                partida.Value.RevelarRiver();
        }

        public void PerguntarPagarFlop()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                IAcaoTomada a = jog.Key.PreFlop(this.Mesa.RegrasMesaAtual.Flop);

                switch(a.Acao)
                {
                    case AcoesDecisaoJogador.PayFlop:
                        jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Flop);
                        jog.Value.AddToPote(this.Mesa.RegrasMesaAtual.Flop);
                        jog.Value.AddToPote(this.Mesa.RegrasMesaAtual.Flop);
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.Flop, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Fold:
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.FimDeJogo, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        this.EncerrarPartidaJogador(jog.Key);
                        break;

                    default: throw new Exception("Ação não esperada.");
                }
            }
        }

        public void PerguntarAumentarPreTurn()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                IAcaoTomada a = jog.Key.Flop(jog.Value.CartasMesa, 0);

                switch (a.Acao)
                {
                    case AcoesDecisaoJogador.Check:
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.Turn, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Call:
                        jog.Value.AddToPote(jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn));
                        jog.Value.AddToPote(jog.Value.Banca.PagaValor(this.Mesa.RegrasMesaAtual.Turn));
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.Turn, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Raise:
                        jog.Value.AddToPote(jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn));
                        jog.Value.AddToPote(jog.Value.Banca.PagaValor(this.Mesa.RegrasMesaAtual.Turn));
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.Turn, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    default: throw new Exception("Ação não esperada.");
                }
            }
        }

        public void PerguntarAumentarPreRiver()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                IAcaoTomada a = jog.Key.Turn(jog.Value.CartasMesa, this.Mesa.RegrasMesaAtual.Turn);

                switch (a.Acao)
                {
                    case AcoesDecisaoJogador.Check:
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.River, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Call:
                        jog.Value.AddToPote(jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn) * 2);
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.River, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Raise:
                        jog.Value.AddToPote(jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn) * 2);
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.River, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    default: throw new Exception("Ação não esperada.");
                }

            }
        }

        public void VerificarGanhadorPartida(IPartida p)
        {
            // Recupera as cartas
            Carta [] CartasBanca = new Carta[] {
                p.Banca.Cartas[0],
                p.Banca.Cartas[1]
            };
            Carta [] CartasJogador = new Carta[] {
                p.Jogador.Cartas[0],
                p.Jogador.Cartas[1]
            };
            Carta[] CartasMesa = p.CartasMesa;

            ConstrutorMelhorMao construtorMao = new ConstrutorMelhorMao();
            MaoTexasHoldem melhorMaoJogador = construtorMao.GetMelhorMao(CartasMesa.Union(CartasJogador).ToList());

            construtorMao = new ConstrutorMelhorMao();
            MaoTexasHoldem melhorMaoBanca = construtorMao.GetMelhorMao(CartasMesa.Union(CartasBanca).ToList());

            switch(melhorMaoJogador.Compara(melhorMaoBanca))
            {
                case -1: 
                    p.JogadorGanhador = VencedorPartida.Banca;
                    p.Banca.RecebeValor(p.PoteAgora);
                    break;

                case 0: 
                    p.JogadorGanhador = VencedorPartida.Empate;
                    p.Banca.RecebeValor(p.PoteAgora / 2);
                    p.Jogador.RecebeValor(p.PoteAgora / 2);
                    break;

                case 1: 
                    p.JogadorGanhador = VencedorPartida.Jogador;
                    p.Jogador.RecebeValor(p.PoteAgora);
                    break;
            }
        }

        public bool ExistePartidaEmAndamento() 
            => this.Mesa.PartidasAtuais.Count > 0;

        public void EncerrarPartidas()
        {
            IList<IJogador> jogadores = this.Mesa.Participantes;

            foreach (IJogador jog in jogadores)
            {
                IPartida p = this.Mesa.PartidasAtuais[jog];

                this.DistribuirCartasBanca(p);
                this.VerificarGanhadorPartida(p);
                this.EncerrarPartidaJogador(jog);
            }
        }

        public void EncerrarPartidaJogador(IJogador j)
        {
            j.AddPartidaHistorico(this.Mesa.PartidasAtuais[j]);
            this.Mesa.PartidasAtuais.Remove(j);
        }

        public IJogador GetBancaPadrao() => this.banca;

        protected void DistribuirCartasBanca(IPartida p) => p.Banca.RecebeCarta(p.PopDeck(), p.PopDeck());

    }
}
