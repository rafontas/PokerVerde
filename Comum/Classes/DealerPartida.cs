using Comum.Excecoes;
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

        //todo: retirar banca e instanciar como padrão
        public DealerPartida(Mesa mesa, IJogador banca)
        { 
            this.Mesa = mesa;
            this.banca = banca;
        }

        public IJogador GetBancaPadrao() => this.banca;

        public bool HaJogadoresParaJogar() => this.Mesa.JogadoresNaMesa.Count > 0;

        public void PrepararNovaPartida() => this.Mesa.ReiniciarMesa();

        public void PergutarQuemIraJogar()
        {
            foreach (IJogador j in this.Mesa.JogadoresNaMesa)
            {
                IAcaoTomada a = j.PreJogo(this.Mesa.RegrasMesaAtual.Ant);

                switch(a.Acao)
                {
                    case AcoesDecisaoJogador.Play:
                        this.Mesa.PartidasAtuais.Add(j, new Partida(j.SeqProximaPartida, j, this.banca));
                        break;

                    case AcoesDecisaoJogador.Fold:
                        break;

                    case AcoesDecisaoJogador.Stop:
                        this.RetirarJogadorDaMesa(j);
                        break;

                    default:
                        throw new JogadorException("Jogador devolveu ação não possível");
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

        public void EncerrarPartidas()
        {
            IList<IJogador> jogadores = this.Mesa.JogadoresNaMesa;

            foreach (IJogador jog in jogadores)
            {
                IPartida p = this.Mesa.PartidasAtuais[jog];

                if (p.Rodadas.Last().TipoRodada == TipoRodada.FimDeJogo) continue;

                this.DistribuirCartasBanca(p);
                this.VerificarGanhadorPartida(p);
                this.EntregarPoteAosVencedores(p);
                this.EncerrarPartidaJogador(jog);
            }
        }

        public void EncerrarPartidasTerminadas()
        {
            //int numeroPartidas = this.Mesa.PartidasAtuais.Count;
            for (int i = 0; i < this.Mesa.PartidasAtuais.Count; i++)
            {
                KeyValuePair<IJogador, IPartida> item = this.Mesa.PartidasAtuais.ElementAt(i);
                
                if(item.Value.Rodadas.Last().TipoRodada == TipoRodada.FimDeJogo)
                {
                    this.EncerrarPartidaJogador(item.Key);
                    i--;
                }
            }
        }

        public void EncerrarPartidaJogador(IJogador j)
        {
            j.AddPartidaHistorico(this.Mesa.PartidasAtuais[j]);
            this.Mesa.PartidasAtuais.Remove(j);
        }

        public bool ExistePartidaEmAndamento() => this.Mesa.PartidasAtuais.Count > 0;

        //todo: tirar isso de public
        public void CobrarAnt(IJogador jogador) => this.Mesa.PartidasAtuais[jogador].AddToPote(jogador.PagarValor(this.Mesa.RegrasMesaAtual.Ant), TipoJogadorTHB.Jogador);

        //todo: tirar isso de public
        public void DistribuirCartasJogadores(IPartida p) => p.Jogador.ReceberCarta(p.PopDeck(), p.PopDeck());

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
                        jogador.PagarValor(this.Mesa.RegrasMesaAtual.Flop);
                        partida.AddToPote(this.Mesa.RegrasMesaAtual.Flop, TipoJogadorTHB.Jogador);
                        partida.AddToPote(this.Mesa.RegrasMesaAtual.Flop, TipoJogadorTHB.Banca);
                        proximaRodada = new RodadaTHB(TipoRodada.Flop, partida.PoteAgora, partida.CartasMesa);
                        break;

                    case AcoesDecisaoJogador.Fold:
                        proximaRodada = new RodadaTHB(TipoRodada.FimDeJogo, partida.PoteAgora, partida.CartasMesa);
                        break;

                    default: throw new Exception("Ação não esperada.");
                }

                partida.AddRodada(proximaRodada);
            }

            this.EncerrarPartidasTerminadas();

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
                        jog.Value.AddToPote(jog.Key.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Jogador);
                        jog.Value.AddToPote(jog.Value.Banca.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Banca);
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.Turn, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Raise:
                        jog.Value.AddToPote(jog.Key.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Jogador);
                        jog.Value.AddToPote(jog.Value.Banca.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Banca);
                        jog.Value.AddRodada(new RodadaTHB(TipoRodada.Turn, jog.Value.PoteAgora, jog.Value.CartasMesa));
                        break;

                    default: throw new Exception("Ação não esperada.");
                }
            }

            this.EncerrarPartidasTerminadas();
        }

        public void PerguntarAumentarPreRiver()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                IAcaoTomada acaoJogador = jog.Key.Turn(jog.Value.CartasMesa, 0);
                IPartida partida = jog.Value;

                switch (acaoJogador.Acao)
                {
                    case AcoesDecisaoJogador.Check:
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Call:
                        partida.AddToPote(jog.Key.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Jogador);
                        partida.AddToPote(jog.Key.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Banca);
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Raise:
                        partida.AddToPote(jog.Key.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Jogador);
                        partida.AddToPote(jog.Key.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Banca);
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
                        break;

                    default: throw new Exception("Ação não esperada.");
                }

            }

            this.EncerrarPartidasTerminadas();
        }

        protected void DistribuirCartasBanca(IPartida p) => p.Banca.ReceberCarta(p.PopDeck(), p.PopDeck());

        //TODO: verificar se melhor maneira de contornar o virtual
        public virtual void VerificarGanhadorPartida(IPartida p)
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

            switch (melhorMaoJogador.Compara(melhorMaoBanca))
            {
                case -1: 
                    p.JogadorGanhador = VencedorPartida.Banca;
                    break;

                case 0: 
                    p.JogadorGanhador = VencedorPartida.Empate;
                    break;

                case 1: 
                    p.JogadorGanhador = VencedorPartida.Jogador;
                    break;

                default:
                    throw new DealerException("Erro ao comparar mão dos jogadores. Retornado valor não previsto.");
            }
        }

        /// <summary>
        /// Distribui o pote de acordo com o final da partida
        /// </summary>
        /// <param name="p">IPartida jogada</param>
        public void EntregarPoteAosVencedores(IPartida p)
        {
            switch (p.JogadorGanhador)
            {
                case VencedorPartida.Banca:
                    p.Banca.ReceberValor(p.PoteAgora);
                    break;

                case VencedorPartida.Empate:
                    p.Banca.ReceberValor(p.ValorInvestidoBanca);
                    p.Jogador.ReceberValor(p.ValorInvestidoJogador);
                    break;

                case VencedorPartida.Jogador:
                    p.Jogador.ReceberValor(p.PoteAgora);
                    break;

                default:
                    throw new DealerException("Erro ao entregar potes aos vencedores. Jogador ganhador não previsto.");
            }
        }

        /// <summary>
        /// Retira jogador da mesa.
        /// </summary>
        /// <param name="j">Jogador a sair.</param>
        public void RetirarJogadorDaMesa(IJogador j)
        {
            if (!this.Mesa.JogadoresNaMesa.Contains(j)) throw new DealerException("Tentando retirar jogador que não está na mesa.");

            this.Mesa.JogadoresNaMesa.Remove(j);
        }
    }
}
