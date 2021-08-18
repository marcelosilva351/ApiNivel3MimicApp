using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Models.DTO_s
{
    public class ReadPalavraDTO
    { 
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Pontuacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime Criacao { get; set; }
        public DateTime? Atualizado { get; set; }
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
