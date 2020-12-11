using Comum.Excecoes;
using Comum.HoldemHand;
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

        public Carta[] MaoMandatoriaBanca { get; private set; } = null;
        public Carta[] MaoMandatoriaJogador { get; private set; } = null;

        private IList<Carta> CartasAhRetirarDeck()
        {
            IList<Carta> cartasAhRetirar = new List<Carta>();

            if (this.MaoMandatoriaJogador != null)
            {
                foreach(var c in this.MaoMandatoriaJogador) 
                    cartasAhRetirar.Add(c);
            }

            if (this.MaoMandatoriaBanca != null)
            {
                foreach(var c in MaoMandatoriaBanca) 
                    cartasAhRetirar.Add(c);
            }

            return (cartasAhRetirar.Count == 0 ? null : cartasAhRetirar);
        }

        //todo: retirar banca e instanciar como padrão
        public DealerPartida(Mesa mesa, IJogador Banca, Carta [] MaoBanca = null, Carta[] MaoJogador = null)
        { 
            this.Mesa = mesa;
            this.banca = Banca;
            this.SetMaosPreferenciais(MaoBanca, MaoJogador);
        }

        private void SetMaosPreferenciais(Carta[] MaoBanca = null, Carta[] MaoJogador = null)
        {
            if (MaoBanca != null)
            {
                this.MaoMandatoriaBanca = new Carta[] { null, null };

                if (MaoBanca.Length >= 1) this.MaoMandatoriaBanca[0] = MaoBanca[0];
                if (MaoBanca.Length >= 2) this.MaoMandatoriaBanca[1] = MaoBanca[1];
            }

            if (MaoJogador != null)
            {
                this.MaoMandatoriaJogador = new Carta[] { null, null };

                if (MaoJogador.Length >= 1) this.MaoMandatoriaJogador[0] = MaoJogador[0];
                if (MaoJogador.Length >= 2) this.MaoMandatoriaJogador[1] = MaoJogador[1];
            }
        }

        public IJogador GetBancaPadrao() => this.banca;

        public bool HaJogadoresParaJogar() => this.Mesa.JogadoresNaMesa.Count > 0;

        public void PrepararNovaPartida() => this.Mesa.ReiniciarMesa();

        public void PergutarQuemIraJogar()
        {
            IList<IJogador> jogadoresPararamDeJogar = new List<IJogador>();
             
            foreach (IJogador j in this.Mesa.JogadoresNaMesa)
            {
                IAcaoTomada a = j.PreJogo(this.Mesa.RegrasMesaAtual.Ant);

                switch(a.Acao)
                {
                    case AcoesDecisaoJogador.Play:
                        this.Mesa.PartidasAtuais.Add(j, new Partida(j.SeqProximaPartida, j, this.banca, CartasAhRetirarDeck()));
                        break;

                    case AcoesDecisaoJogador.Stop:
                        jogadoresPararamDeJogar.Add(j);
                        break;

                    default:
                        throw new JogadorException("Jogador devolveu ação não possível");
                }
            }

            foreach(IJogador j in jogadoresPararamDeJogar)
                this.RetirarJogadorDaMesa(j);
        }

        public void ExecutarPreFlop()
        {
            foreach(var jogadorPartida in this.Mesa.PartidasAtuais)
            {
                this.CobrarAnt(jogadorPartida.Value);
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
            IPartida partidaAtual = this.Mesa.PartidasAtuais[j];
            this.VerificarGanhadorPartida(partidaAtual);
            j.AddPartidaHistorico(this.Mesa.PartidasAtuais[j]);
            this.Mesa.PartidasAtuais.Remove(j);
        }

        public bool ExistePartidaEmAndamento() => this.Mesa.PartidasAtuais.Count > 0;

        //todo: tirar isso de public
        public void CobrarAnt(IPartida partida)
        {
            partida.AddToPote(partida.Jogador.PagarValor(this.Mesa.RegrasMesaAtual.Ant), TipoJogadorTHB.Jogador);
        } 

        //todo: tirar isso de public

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
                IJogador banca = partida.Banca;
                IRodada rodadaAtual = partida.Rodadas.Last();
                IRodada proximaRodada;

                IAcaoTomada acaoTomada = jogador.PreFlop(this.Mesa.RegrasMesaAtual.Flop);
                rodadaAtual.AddAcaoTomada(acaoTomada);

                switch (acaoTomada.Acao)
                {
                    case AcoesDecisaoJogador.PayFlop:
                        partida.AddToPote(jogador.PagarValor(this.Mesa.RegrasMesaAtual.Flop), TipoJogadorTHB.Jogador);
                        partida.AddToPote(banca.PagarValor(this.Mesa.RegrasMesaAtual.Flop), TipoJogadorTHB.Banca);
                        proximaRodada = new RodadaTHB(TipoRodada.Flop, partida.PoteAgora, partida.CartasMesa);
                        break;

                    case AcoesDecisaoJogador.Fold:
                        proximaRodada = new RodadaTHB(TipoRodada.FimDeJogo, partida.PoteAgora, partida.CartasMesa);
                        break;

                    default: 
                        throw new Exception("Ação não esperada.");
                }

                partida.AddRodada(proximaRodada);
            }

            this.EncerrarPartidasTerminadas();
        }

        public void PerguntarAumentarPreTurn()
        {
            foreach (var jog in this.Mesa.PartidasAtuais)
            {
                IPartida partida = jog.Value;
                IJogador jogador = jog.Key;
                IJogador banca = partida.Banca;
                IAcaoTomada a = jogador.Flop(jog.Value.CartasMesa, 0);

                switch (a.Acao)
                {
                    case AcoesDecisaoJogador.Check:
                        partida.AddRodada(new RodadaTHB(TipoRodada.Turn, partida.PoteAgora, partida.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Call:
                        partida.AddToPote(jogador.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Jogador);
                        partida.AddToPote(banca.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Banca);
                        partida.AddRodada(new RodadaTHB(TipoRodada.Turn, partida.PoteAgora, partida.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Raise:
                        partida.AddToPote(jogador.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Jogador);
                        partida.AddToPote(banca.PagarValor(this.Mesa.RegrasMesaAtual.Turn), TipoJogadorTHB.Banca);
                        partida.AddRodada(new RodadaTHB(TipoRodada.Turn, partida.PoteAgora, partida.CartasMesa));
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
                IJogador jogador = jog.Key;
                IPartida partida = jog.Value;
                IJogador banca = partida.Banca;
                IAcaoTomada acaoJogador = jogador.Turn(jog.Value.CartasMesa, 0);

                switch (acaoJogador.Acao)
                {
                    case AcoesDecisaoJogador.Check:
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Call:
                        partida.AddToPote(jogador.PagarValor(this.Mesa.RegrasMesaAtual.River), TipoJogadorTHB.Jogador);
                        partida.AddToPote(banca.PagarValor(this.Mesa.RegrasMesaAtual.River), TipoJogadorTHB.Banca);
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
                        break;

                    case AcoesDecisaoJogador.Raise:
                        partida.AddToPote(jogador.PagarValor(this.Mesa.RegrasMesaAtual.River), TipoJogadorTHB.Jogador);
                        partida.AddToPote(banca.PagarValor(this.Mesa.RegrasMesaAtual.River), TipoJogadorTHB.Banca);
                        partida.AddRodada(new RodadaTHB(TipoRodada.River, partida.PoteAgora, partida.CartasMesa));
                        break;

                    default: throw new Exception("Ação não esperada.");
                }

            }

            this.EncerrarPartidasTerminadas();
        }

        protected void DistribuirCartasBanca(IPartida p) => this.DistribuiCartas(p.Banca, this.MaoMandatoriaBanca, p);

        public void DistribuirCartasJogadores(IPartida p) => this.DistribuiCartas(p.Jogador, this.MaoMandatoriaJogador, p);

        private void DistribuiCartas(IJogador jogador, Carta[] CartasMandatorias, IPartida p)
        {
            if (CartasMandatorias != null && CartasMandatorias.Length > 0)
            {
                if (CartasMandatorias.Length == 1)
                {
                    jogador.ReceberCarta(CartasMandatorias[0].Clone(), p.PopDeck());
                }
                else
                {
                    jogador.ReceberCarta(CartasMandatorias[0].Clone(), CartasMandatorias[1].Clone());
                }
            }
            else
            {
                jogador.ReceberCarta(p.PopDeck(), p.PopDeck());
            }
        }

        //TODO: verificar se melhor maneira de contornar o virtual
        public virtual void VerificarGanhadorPartida(IPartida p)
        {
            int jogadorVencedor;
            TipoRodada tipoRodada = p.Rodadas.Last().TipoRodada;

            if ((tipoRodada == TipoRodada.PreFlop) ||
                (tipoRodada == TipoRodada.Flop) ||
                (tipoRodada == TipoRodada.Turn) ||
                (tipoRodada == TipoRodada.FimDeJogo && p.Rodadas.Count < 5))
            {
                jogadorVencedor = -1;
            }
            else if (tipoRodada == TipoRodada.River)
            {
                jogadorVencedor = this.RetornaMelhorMaoPartidaFinalizada(p);
            }
            else
            {
                throw new DealerException("Passada partida em rodada errada para atribuir vencedor.");
            }

            switch (jogadorVencedor)
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
        
        private int RetornaMelhorMaoPartidaFinalizada_antiga(IPartida p) 
        {
            if (p.Rodadas.Last().TipoRodada != TipoRodada.River)
                throw new DealerException("Rodada inválida para avaliação de mãos.");

            // Recupera as cartas
            Carta[] CartasBanca = new Carta[] {
                p.Banca.Cartas[0],
                p.Banca.Cartas[1]
            };
            Carta[] CartasJogador = new Carta[] {
                p.Jogador.Cartas[0],
                p.Jogador.Cartas[1]
            };
            Carta[] CartasMesa = p.CartasMesa;

            ConstrutorMelhorMao construtorMao = new ConstrutorMelhorMao();
            MaoTexasHoldem melhorMaoJogador = construtorMao.GetMelhorMao(CartasMesa.Union(CartasJogador).ToList());

            construtorMao = new ConstrutorMelhorMao();
            MaoTexasHoldem melhorMaoBanca = construtorMao.GetMelhorMao(CartasMesa.Union(CartasBanca).ToList());

            return melhorMaoJogador.Compara(melhorMaoBanca);
        }

        private int RetornaMelhorMaoPartidaFinalizada(IPartida p)
        {
            if (p.Rodadas.Last().TipoRodada != TipoRodada.River)
                throw new DealerException("Rodada inválida para avaliação de mãos.");

            string cartasJogador = String.Empty, cartasBanca = String.Empty, mesa = String.Empty;

            foreach (var c in p.CartasMesa)
            {
                mesa += c.ToFastCard() + " ";
            }

            cartasBanca = p.Banca.Cartas[0].ToFastCard() + " " + p.Banca.Cartas[1].ToFastCard();
            cartasJogador = p.Jogador.Cartas[0].ToFastCard() + " " + p.Jogador.Cartas[1].ToFastCard();


            Hand jogador = new Hand(cartasJogador, mesa);
            Hand banca = new Hand(cartasBanca, mesa);

            return (jogador > banca ? 1 : // jogador vencedor
                        (banca > jogador ?  
                            -1 : // banca vencedora
                            0)); // empate
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
