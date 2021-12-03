using EcommerceAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Servicos
{
    public class ProdutoService
    {
        private readonly List<Produto> _produto;
        public ProdutoService()
        {
            _produto ??= new List<Produto>();
        }


        public Produto Cadastrar(Produto produto)
        {
            _produto.Add(produto);
            return produto;
        }

        public IEnumerable<Produto> Get()
        {
            return _produto;
        }
        public Produto Get(Guid id)
        {
            return _produto.Where(p => p.Id == id).SingleOrDefault();
        }
        public Produto Atualizar(Guid id, Produto produto) 
        {
            var prod = _produto.Where(p => p.Id == id).SingleOrDefault();

            prod.Atualizar(produto);

            return prod;
        }
        public bool Deletar(Guid id) 
        {
            var prod = _produto.Where(p => p.Id == id).SingleOrDefault();

            if(prod == null) return false;
            _produto.Remove(prod);
            return true;
        }
    }
}
