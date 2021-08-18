using API1.Helpers;
using API1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Repository
{
    public interface IPalavraRepository
    {
        PaginacaoLista<Palavra> ObterTodos(PalavraUrlQuery palavraUrlQuery);
        Palavra Obter(int id);
        void Atualizar();
        void Deletar(Palavra palavra);
        void Cadastrar(Palavra palavra);

    }
}
