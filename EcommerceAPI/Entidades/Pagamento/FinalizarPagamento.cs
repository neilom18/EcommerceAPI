using EcommerceAPI.Enumeradores;
using System;

namespace EcommerceAPI.Entidades.Pagamento
{
    public class FinalizarPagamento
    {
        public FinalizarPagamento(EFormaPagamento formaPagamento, CartaoDebito cartaoDebito, decimal valor)
        {
            Id = Guid.NewGuid();
            FormaPagamento = formaPagamento;
            CartaoDebito = cartaoDebito;
            Valor = valor;
        }

        public FinalizarPagamento(EFormaPagamento formaPagamento, CartaoCredito cartaoCredito, decimal valor)
        {
            Id = Guid.NewGuid();
            FormaPagamento = formaPagamento;
            CartaoCredito = cartaoCredito;
            Valor = valor;
        }

        public FinalizarPagamento(EFormaPagamento formaPagamento, Pix pix, decimal valor)
        {
            Id = Guid.NewGuid();
            FormaPagamento = formaPagamento;
            Pix = pix;
            Valor = valor;
        }

        public Guid Id { get;private set; }
        public EFormaPagamento FormaPagamento { get;private set; }
        public Pix? Pix { get;private set; }
        public CartaoCredito? CartaoCredito { get;private set; }
        public CartaoDebito? CartaoDebito { get;private set; }
        public decimal Valor { get;private set; }
        public bool Valido { get;private set; }

        public void Validar(Pedido pedido)
        {
            switch (FormaPagamento)
            {
                case EFormaPagamento.CartaoCredito: // Tá valido confia
                    Valido = true;
                    if(CartaoCredito == null) Valido = false;
                    if(CartaoCredito.Limite < pedido.Preco) Valido = false;
                    if(CartaoCredito.Titular.Length < 4) Valido = false;
                    if (Valor != pedido.Preco) Valido = false;
                    break;

                case EFormaPagamento.CartaoDebito: // Tá valido confia
                    Valido = true;
                    if(CartaoDebito == null) Valido = false;
                    if(CartaoDebito.Saldo < pedido.Preco) Valido = false;
                    if(CartaoDebito.Titular.Length < 4) Valido = false;
                    if (Valor != pedido.Preco) Valido = false;
                    break;

                case EFormaPagamento.Pix: // Tá valido confia
                    Valido=true;
                    if(Pix == null) Valido = false;
                    if(Pix.NomeTitular.Length < 4) Valido = false;
                    if(Pix.NumeroConta.Length < 8) Valido = false;
                    if (Valor != pedido.Preco) Valido = false;
                    break;

            }
        }
    }
}
