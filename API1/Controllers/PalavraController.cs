using API1.Data;
using API1.Helpers;
using API1.Models;
using API1.Models.DTO_s;
using API1.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Controllers
{
    [ApiController]
    [Route("api/palavras")]
    public class PalavraController : ControllerBase
    {
        private readonly IPalavraRepository _palavraRepository;
        private readonly IMapper _mapper;
        public PalavraController(IPalavraRepository palavraRepository, IMapper mapper)
        {
            _palavraRepository = palavraRepository;
            _mapper = mapper;
        }


        [HttpGet("", Name = "ObterTodas")]
        public IActionResult ObterTodas([FromQuery] PalavraUrlQuery palavraUrlQuery)
        {
            try
            {
                var palavras = _palavraRepository.ObterTodos(palavraUrlQuery);
                var lista = _mapper.Map<PaginacaoLista<ReadPalavraDTO>>(palavras);
                foreach (var item in lista.Palavras)
                {
                    item.Links.Add(new LinkDTO("Self", Url.Link("ObterPalavra", new { Id = item.Id }), "GET"));
                    item.Links.Add(new LinkDTO("Update", Url.Link("AtualizarPalavra", new { Id = item.Id }), "PUT"));
                    item.Links.Add(new LinkDTO("delete", Url.Link("DeletarPalavra", new { Id = item.Id }), "DELETE"));
                }

                lista.LinksPaginacao.Add(new LinkDTO("Self", Url.Link("ObterTodas",palavraUrlQuery), "ObterTodas"));

                //Enviando paginação no header
                if (palavraUrlQuery.NumeroPagina != null)
                {
                    Response.Headers.Add("Pagination-X", JsonConvert.SerializeObject(palavras.Paginacao));

                    //Pagina que não existe
                    if (palavraUrlQuery.NumeroPagina > palavras.Paginacao.TotalPaginas)
                    {
                        return NotFound();
                    }

                    PalavraUrlQuery palavraUrlQueryPaginacao = new PalavraUrlQuery();
                    palavraUrlQueryPaginacao.NumeroPagina = lista.Paginacao.NumeroPagina;
  
                    int proxPagina = palavraUrlQueryPaginacao.NumeroPagina.Value + 1;
                    int paginaAnterior = palavraUrlQueryPaginacao.NumeroPagina.Value - 1;

                    if (proxPagina <= lista.Paginacao.TotalPaginas)
                    {
                        palavraUrlQueryPaginacao.NumeroPagina = proxPagina;
                        palavraUrlQueryPaginacao.RegistrosPorPagina = lista.Paginacao.RegistroPorPagina;
                        lista.LinksPaginacao.Add(new LinkDTO("Next", Url.Link("ObterTodas", palavraUrlQueryPaginacao), "GET"));
                    }
                    if(paginaAnterior > 0)
                    {
                        palavraUrlQueryPaginacao.NumeroPagina = paginaAnterior;
                        palavraUrlQueryPaginacao.RegistrosPorPagina = lista.Paginacao.RegistroPorPagina;
                        lista.LinksPaginacao.Add(new LinkDTO("Back", Url.Link("ObterTodas", palavraUrlQueryPaginacao), "GET"));
                    }
                    return Ok(lista);

                }
                return Ok(lista);
            }
            catch (SqlException)
            {
                return StatusCode(500, "Erro no banco de dados");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


        [HttpGet("{id}", Name = "ObterPalavra")]
        public IActionResult Obter(int id)
        {

            Palavra palavraById = _palavraRepository.Obter(id);
            if (palavraById == null)
            {
                return NotFound();
            }

            ReadPalavraDTO readPalavraDTO = _mapper.Map<ReadPalavraDTO>(palavraById);
            readPalavraDTO.Links.Add(new LinkDTO("Self", Url.Link("ObterPalavra", new { Id = readPalavraDTO.Id }), "GET"));
            readPalavraDTO.Links.Add(new LinkDTO("Update", Url.Link("AtualizarPalavra", new { Id = readPalavraDTO.Id }), "PUT"));
            readPalavraDTO.Links.Add(new LinkDTO("delete", Url.Link("DeletarPalavra", new { Id = readPalavraDTO.Id }), "DELETE"));


            return Ok(readPalavraDTO);
        }

        [Route("")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] CreatePalavraDTO palavra)
        {
            Palavra palavraADD = _mapper.Map<Palavra>(palavra);
            _palavraRepository.Cadastrar(palavraADD);
            return CreatedAtAction(nameof(Obter), new { Id = palavraADD.Id }, palavra);
        }


        [HttpPut("{id}", Name = "AtualizarPalavra")]

        public IActionResult Atualizar(int id, [FromBody] PutPalavraDTO palavra)
        {
            Palavra obterPalavra = _palavraRepository.Obter(id);
            if (obterPalavra == null)
            {
                return NotFound();
            }
            _mapper.Map(palavra, obterPalavra);
            try
            {
                _palavraRepository.Atualizar();
            } catch (Exception e)
            {
                StatusCode(500, e.Message);
            }
            return NoContent();

        }


        [HttpDelete("{id}", Name = "DeletarPalavra")]
        
        public IActionResult Deletar(int id)
        {

            Palavra palavraRemover = _palavraRepository.Obter(id);
            if(palavraRemover == null)
            {
                return NotFound();
            }
            _palavraRepository.Deletar(palavraRemover);
            return NoContent();
        }







    }
}
