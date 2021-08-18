using API1.Data;
using API1.Helpers;
using API1.Models;
using API1.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Repository
{
    public class PalavraRepository : IPalavraRepository
    {
        private readonly MimicContext _context;

        public PalavraRepository(MimicContext context)
        {
            _context = context;
        }

        public void Atualizar()
        {
            _context.SaveChanges();
        }

  
        public void Cadastrar(Palavra palavra)
        {
            _context.Add(palavra);
            _context.SaveChanges();
        }

        public void Deletar(Palavra palavrva)
        {
            palavrva.Ativo = false;
            _context.SaveChanges();
        }

        public Palavra Obter(int id)
        {
            var palavra = _context.Palavras.Find(id);
            return palavra;
        }

        public PaginacaoLista<Palavra> ObterTodos(PalavraUrlQuery palavraUrlQuery)
        {
                      
            var palavras = _context.Palavras;
            var paginacaoLista = new PaginacaoLista<Palavra>();
      

            //Retorna palavras criadas apos a data que vier na query string
            if (palavraUrlQuery.UltimaAtualizacaoApp != null)
            {
                var palavrarAtualizadasRetorno = palavras.Where(palavra => palavra.Criacao > palavraUrlQuery.UltimaAtualizacaoApp);
                paginacaoLista.Palavras.AddRange(palavrarAtualizadasRetorno);
                return paginacaoLista;
            }
            //Retorna palavras dependendo da paginação
            if (palavraUrlQuery.NumeroPagina.HasValue)
            {
                if(palavraUrlQuery.RegistrosPorPagina == null)
                {
                    throw new Exception("Não é possivel passar o numero de paginas sem dizer quantos registros vão ter em cada pagina");
                }
                else
                {
                    int skip = (palavraUrlQuery.NumeroPagina.Value - 1) * (palavraUrlQuery.RegistrosPorPagina.Value);
                    var palavrasPorPagina = palavras.Skip(skip).Take(palavraUrlQuery.RegistrosPorPagina.Value);
                    var totalDeRegistros = _context.Palavras.Count();


                    var paginacao = new Paginacao();
                    paginacao.NumeroPagina = palavraUrlQuery.NumeroPagina.Value;
                    paginacao.RegistroPorPagina = palavraUrlQuery.RegistrosPorPagina.Value;
                    paginacao.TotalRegistros = totalDeRegistros;
                    paginacao.TotalPaginas = (int)Math.Ceiling((double)totalDeRegistros / palavraUrlQuery.RegistrosPorPagina.Value);

                  
                    paginacaoLista.Paginacao = paginacao;
                    paginacaoLista.Palavras.AddRange(palavrasPorPagina);
                    return paginacaoLista;
                }
            }


            //retorna as palavras se a query string vier null
            paginacaoLista.Palavras.AddRange(palavras);
            return paginacaoLista;
          
               
               
            }
        }

    }
    

