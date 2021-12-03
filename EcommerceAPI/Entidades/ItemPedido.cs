using EcommerceAPI.DTOs;
using System;

namespace EcommerceAPI.Entidades
{
    public class ItemPedido
    {
        public Produto Produto { get;private set; }
        public decimal Preco { get;private set; }
        public int Quantidade { get;private set; }
        public Guid Id { get; set; }
        public ItemPedido(Produto produto, int quantidade)
        {
            Id = Guid.NewGuid();
            Produto = produto;
            Quantidade = quantidade;
        }
        public void SetPreco(int quantidade)
        {
            Quantidade = quantidade;
            Preco = Produto.Preco * quantidade;
        }
    }
}
