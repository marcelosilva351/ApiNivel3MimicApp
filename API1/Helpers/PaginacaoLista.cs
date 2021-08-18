using API1.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Helpers
{
    public class PaginacaoLista<T> 
    {
        public List<T> Palavras { get; set; } = new List<T>();
        public Paginacao Paginacao { get; set; }
        public List<LinkDTO> LinksPaginacao { get; set; } = new List<LinkDTO>();
    }
}
