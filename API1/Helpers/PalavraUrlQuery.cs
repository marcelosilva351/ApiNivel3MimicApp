using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Helpers
{
    public class PalavraUrlQuery
    { 
        public DateTime? UltimaAtualizacaoApp { get; set; }
        public int? NumeroPagina { get; set; }
        public int? RegistrosPorPagina { get; set; }
    }
}
