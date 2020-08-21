using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PokerDAO
{
    [Table("probabilidade.analiseConvergencia")]
    public class AnaliseConvergencia
    {
        [Key]
        private int Id { get; set; }

        public int NumeroDeCartas { get; set; }
        
        public int QuantidadeJogosSimulados { get; set; }
        
        public int ProbabilidadeVitoria { get; set; }


    }
}
