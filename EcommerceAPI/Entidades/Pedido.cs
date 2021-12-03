using EcommerceAPI.Entidades.Pagamento;
using EcommerceAPI.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Entidades
{
    public class Pedido : EntidadeBase
    {
        public Pedido():base(Guid.NewGuid()) { }
        public Pedido(Cliente cliente):base(Guid.NewGuid())
        {
            this.Cliente = cliente;
            ItemPedido = new List<ItemPedido>();
        }

        public Cliente Cliente { get;private set; }
        public List<ItemPedido>? ItemPedido { get;private set; }
        public decimal Preco { get; private set; }
        public bool Finalizado { get; private set; } = false;

        public EFormaPagamento FormaPagamento { get;private set; }

        public void CalcularPreco()
        {
            this.Preco = ItemPedido.Sum(x => x.Preco);
        }
        public void SetPagamento(FinalizarPagamento pagamento)
        {
            if (pagamento.Valido)
            {
                Finalizado = true;
                FormaPagamento = pagamento.FormaPagamento;
            }
        }
    }
}
