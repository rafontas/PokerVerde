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
                this.Mesa.PartidasAtuais[jogador].AddToPote(jogador.PagaValor(this.Mesa.RegrasMesaAtual.Ant), TipoJogadorTHB.Jogador);


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
                IPartida partida = jog.Value;
                IJogador jogador = jog.Key;
                IRodada rodadaAtual = partida.Rodadas.Last();
                IRodada proximaRodada;

                IAcaoTomada acaoTomada = jogador.PreFlop(this.Mesa.RegrasMesaAtual.Flop);
                rodadaAtual.AddAcaoTomada(acaoTomada);

                switch (acaoTomada.Acao)
                {
                    case AcoesDecisaoJogador.PayFlop:
                        jogador.PagaValor(this.Mesa.RegrasMesaAtual.Flop);
                        partida.AddToPote(this.Mesa.RegrasMesaAtual.Flop, TipoJogadorTHB.Jogador);
                        partida.AddToPote(this.Mesa.RegrasMesaAtual.Flop, TipoJogadorTHB.Banca);
                        proximaRodada = new RodadaTHB(TipoRodada.Flop, partida.PoteAgora, partida.CartasMesa);
                        break;

                    case AcoesDecisaoJogador.Fold:
                        proximaRodada = new RodadaTHB(TipoRodada.FimDeJogo, partida.PoteAgora, partida.CartasMesa);
                        this.EncerrarPartidaJogador(jogador);
                        break;

                    default: throw new Exception("Ação não esperada.");
                }

                partida.AddRodada(proximaRodada);
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
                        jog.Value.AddToPote(jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Jogador);
                        jog.Value.AddToPote(jog.Value.Banca.PagaValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Banca);
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.Turn, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Raise:
                        jog.Value.AddToPote(jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Jogador);
                        jog.Value.AddToPote(jog.Value.Banca.PagaValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Banca);
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
                IAcaoTomada acaoJogador = jog.Key.Turn(jog.Value.CartasMesa, this.Mesa.RegrasMesaAtual.Turn);
                IPartida partida = jog.Value;
                //IRodada rodada = new RodadaTHB(TipoRodada.River, );

                switch (acaoJogador.Acao)
                {
                    case AcoesDecisaoJogador.Check:
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Call:
                        partida.AddToPote(jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn) * 2, TipoJogadorTHB.Jogador);
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Raise:
                        partida.AddToPote(jog.Key.PagaValor(this.Mesa.RegrasMesaAtual.Turn) * 2, TipoJogadorTHB.Jogador);
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
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
                    p.Banca.RecebeValor(p.ValorInvestidoBanca);
                    p.Jogador.RecebeValor(p.ValorInvestidoJogador);
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
