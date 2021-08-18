using API1.Helpers;
using API1.Models;
using API1.Models.DTO_s;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Profiles
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Palavra, ReadPalavraDTO>();
            CreateMap<CreatePalavraDTO, Palavra>();
            CreateMap<PutPalavraDTO, Palavra>();
            CreateMap<PaginacaoLista<Palavra>, PaginacaoLista<ReadPalavraDTO>>();
        }
    }
}
