using EcommerceAPI.Enumeradores;
using System;

namespace EcommerceAPI.DTOs
{
    public class FinalizarPagamentoDTO
    {
        public EFormaPagamento FormaPagamento { get; set; }
        public PixDTO? PixDTO { get; set; }
        public CartaoCreditoDTO? CartaoCreditoDTO { get; set;}
        public CartaoDebitoDTO? CartaoDebitoDTO { get; set; }
    }
}
