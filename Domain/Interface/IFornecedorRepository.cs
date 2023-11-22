﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IFornecedorRepository
    {

       // public Task<Fornecedor> ObterPorId(int id);
       // public Task<Fornecedor> ObterPorCnpj(string cnpj);
       // public Task<IEnumerable<Fornecedor>> ObterTodos();

        
        Task Adicionar(Fornecedor fornecedor);
        public Task Atualizar(Fornecedor fornecedor);
        public Task Remover(Fornecedor fornecedor);
        Task<Fornecedor> ObterPorId(Guid id);
        IEnumerable<Fornecedor> ObterPorNome(string nome);
        Task Desativar(Fornecedor fornecedor);
        Task Ativar(Fornecedor fornecedor);

    }
}
