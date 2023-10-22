﻿
using AutoMapper;
using Data.Providers.MongoDb.Collections;
using Data.Providers.MongoDb.Interfaces;
using Domain;
using Domain.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoRepository<ProdutoCollection> _produtoRepository;
        private readonly IMapper _mapper;

        #region - Construtores
        public ProdutoRepository(IMongoRepository<ProdutoCollection> produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        #endregion
        #region - Funções
        public async Task Adicionar(Produto produto)
        {
            await _produtoRepository.InsertOneAsync(_mapper.Map<ProdutoCollection>(produto));
        }

        public async Task Atualizar(Produto produto)
        {
            var existingProduct = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId).FirstOrDefault();

            if (existingProduct == null)
            {
                throw new ApplicationException("Produto não encontrado");
            }

            // Atualize as propriedades do registro existente com base no objeto Produto fornecido
            existingProduct.Nome = produto.Nome;
            existingProduct.Descricao = produto.Descricao;
            existingProduct.Ativo = produto.Ativo;
            existingProduct.Valor = produto.Valor;
            existingProduct.QuantidadeEstoque = produto.QuantidadeEstoque;

            // Execute a operação de atualização no banco de dados
            await _produtoRepository.ReplaceOneAsync(existingProduct);
        }

        public async Task Desativar(Produto produto)
        {

            var buscaProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);

            if (buscaProduto == null) throw new ApplicationException("Não é possível desativar um produto que não existe");

            var produtoCollection = _mapper.Map<ProdutoCollection>(produto);

            produtoCollection.Id = buscaProduto.FirstOrDefault().Id;

            await _produtoRepository.ReplaceOneAsync(produtoCollection);
        }
        public async Task DebitarEstoque(Produto produto)
        {

            var buscaProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);

            if (buscaProduto == null) throw new ApplicationException("Não é possível desativar um produto que não existe");

            var produtoCollection = _mapper.Map<ProdutoCollection>(produto);

            produtoCollection.Id = buscaProduto.FirstOrDefault().Id;

            await _produtoRepository.ReplaceOneAsync(produtoCollection);
        }
        public async Task ReporEstoque(Produto produto)
        {

            var buscaProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);

            if (buscaProduto == null) throw new ApplicationException("Não é possível desativar um produto que não existe");

            var produtoCollection = _mapper.Map<ProdutoCollection>(produto);

            produtoCollection.Id = buscaProduto.FirstOrDefault().Id;

            await _produtoRepository.ReplaceOneAsync(produtoCollection);
        }

        public Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> ObterPorId(Guid id)
        {

            var buscaProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == id);

            var produto = _mapper.Map<Produto>(buscaProduto.FirstOrDefault());

            return produto;
        }

        public IEnumerable<Produto> ObterTodos()
        {
            var produtoList = _produtoRepository.FilterBy(filter => true);

            return _mapper.Map<IEnumerable<Produto>>(produtoList);

        }
        public IEnumerable<Produto> ObterPorNome(string nome)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.Nome.Contains(nome));

            if (buscaProduto == null || !buscaProduto.Any())
            {
                // Se não houver produtos encontrados, você pode retornar uma lista vazia ou lançar uma exceção apropriada.
                // Aqui, estou retornando uma lista vazia como exemplo.
                return new List<Produto>();
            }

            var produtosViewModel = _mapper.Map<IEnumerable<Produto>>(buscaProduto);

            return produtosViewModel;
        }
    

        public async Task Ativar(Produto produto)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == produto.CodigoId);

            if (buscaProduto == null) throw new ApplicationException("Não é possível ativar um produto que não existe");

            var produtoCollection = _mapper.Map<ProdutoCollection>(produto);

            produtoCollection.Id = buscaProduto.FirstOrDefault().Id;

            await _produtoRepository.ReplaceOneAsync(produtoCollection);
        }
        public async Task AtualizarValor(Guid id, decimal novoValor)
        {
            var buscaProduto = _produtoRepository.FilterBy(filter => filter.CodigoId == id);

            var produtoCollection = buscaProduto.FirstOrDefault();

            if (produtoCollection == null) throw new ApplicationException("Produto não encontrado");

            produtoCollection.Valor = novoValor;

            await _produtoRepository.ReplaceOneAsync(produtoCollection);
        }



        #endregion

    }

}