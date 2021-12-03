
using EcommerceAPI.Entidades;
using EcommerceAPI.Entidades.Pagamento;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Servicos
{
    public class PedidoService
    {
        private readonly ProdutoService _produtos;
        private readonly List<Pedido> _pedidos;
        public PedidoService(ProdutoService produtoService)
        {
            _produtos = produtoService;
            _pedidos = new List<Pedido>();
        }
        public IEnumerable<Pedido> Get()
        {
            return _pedidos;
        }
        public Pedido Get(Guid id)
        {
            return _pedidos.Where(x => x.Id == id).FirstOrDefault();
        }
        public Pedido Adicionar(Pedido pedido)
        {
            _pedidos.Add(pedido);
            return pedido;
        }
        public Pedido AdicionarItem(Guid id, ItemPedido item)
        {
            var pedido = _pedidos.Where(p => p.Id == id).SingleOrDefault();
            if (pedido is null)
                throw new ArgumentException("Pedido não existe!");
            var prod = _produtos.Get(item.Produto.Id);
            if (prod is null)
                throw new Exception("Produto não existe!");
            if (prod.Preco != item.Produto.Preco)
                throw new Exception("Produto com valor inválido!");
            pedido.ItemPedido.Add(item);
            item.SetPreco(item.Quantidade);
            pedido.CalcularPreco();
            return pedido;
        }
        public Pedido AtualizarItem(Guid id, Guid itemId,int qnt)
        {
            var pedido = _pedidos.Where(p => p.Id == id).SingleOrDefault();
            if (pedido is null)
                throw new ArgumentException("Pedido não existe!");
            var prod = pedido.ItemPedido.Where(x => x.Produto.Id == itemId).SingleOrDefault();
            if (prod is null)
                throw new Exception("Produto não existe!");
            prod.SetPreco(qnt);
            pedido.CalcularPreco();
            return pedido;
        }
        public string Deletar(Guid id, Guid itemId)
        {
            var pedido = _pedidos.Where(p => p.Id == id).SingleOrDefault();
            if (pedido is null)
                throw new ArgumentException("Pedido não existe!");
            var prod = pedido.ItemPedido.Where(x => x.Produto.Id == itemId).SingleOrDefault();
            if (prod is null)
                throw new Exception("Produto não existe!");
            pedido.ItemPedido.Remove(prod);
            pedido.CalcularPreco();
            return "item removido com sucesso!";
        }
        public string Pagamento(Guid id, FinalizarPagamento pagamento)
        {
            var pedido = _pedidos.Where(p => p.Id == id).SingleOrDefault();
            pagamento.Validar(pedido);
            pedido.SetPagamento(pagamento);

            if (pagamento.Valido)
                // Esperaria confirmação de pagamento do Banco
                return "Pagamento finalizado com sucesso!!!";
            throw new ArgumentException("Ocorreu um erro no Pagamento, tente mais tarde!");
        }
    }
}
