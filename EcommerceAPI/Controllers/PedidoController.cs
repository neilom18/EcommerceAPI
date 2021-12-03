using EcommerceAPI.DTOs;
using EcommerceAPI.Entidades;
using EcommerceAPI.Entidades.Pagamento;
using EcommerceAPI.Enumeradores;
using EcommerceAPI.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EcommerceAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;
        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pedidoService.Get());
        }
        [HttpGet, Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_pedidoService.Get(id));
        }
        [HttpPost, Route("{id}/item")]
        public IActionResult AddItem(Guid id,ItemPedidoDTO itemDTO)
        {
            itemDTO.Validar();
            if (!itemDTO.Valido) return BadRequest();
            var item = new ItemPedido(
                new Produto
                (id: itemDTO.ProdutoDTO.Id.Value,
                nome: itemDTO.ProdutoDTO.Nome,
                descricao: itemDTO.ProdutoDTO.Descricao,
                preco: itemDTO.ProdutoDTO.Preco)
                ,quantidade: itemDTO.Quantidade);
            return Ok(_pedidoService.AdicionarItem(id, item));
        }
        [HttpPut, Route("{id}/{itemId}")]
        public IActionResult PutItem(Guid id, Guid itemId,int qnt)
        {
            return Ok(_pedidoService.AtualizarItem(id, itemId, qnt));
        }
        [HttpDelete, Route("{id}/{itemId}")]
        public IActionResult Delete(Guid id, Guid itemId)
        {
            return Ok(_pedidoService.Deletar(id, itemId));
        }
        [HttpPost, Route("{id}/pagamento")]
        public IActionResult Pagamento(Guid id, FinalizarPagamentoDTO finalizarPagamentoDTO, decimal valor)
        {
            if(finalizarPagamentoDTO == null)
                return BadRequest();
            switch (finalizarPagamentoDTO.FormaPagamento)
            {
                case EFormaPagamento.CartaoCredito:
                        finalizarPagamentoDTO.CartaoCreditoDTO.Validar();
                        var pagamentoCredito = new FinalizarPagamento(
                            formaPagamento: finalizarPagamentoDTO.FormaPagamento,
                            valor: valor,
                            cartaoCredito: new CartaoCredito(cVV: finalizarPagamentoDTO.CartaoCreditoDTO.CVV,
                        numero: finalizarPagamentoDTO.CartaoCreditoDTO.Numero,
                        titular: finalizarPagamentoDTO.CartaoCreditoDTO.Titular,
                        limite: finalizarPagamentoDTO.CartaoCreditoDTO.Limite,
                        validade: finalizarPagamentoDTO.CartaoCreditoDTO.Validade));
                        if (pagamentoCredito is null) return BadRequest();
                        return Ok(_pedidoService.Pagamento(id,pagamentoCredito));
                    
                case EFormaPagamento.CartaoDebito:
                    finalizarPagamentoDTO.CartaoDebitoDTO.Validar();
                    var pagamentoDebito = new FinalizarPagamento(
                        formaPagamento: finalizarPagamentoDTO.FormaPagamento,
                        valor: valor,
                        cartaoDebito: new CartaoDebito(cVV: finalizarPagamentoDTO.CartaoDebitoDTO.CVV,
                        numero: finalizarPagamentoDTO.CartaoDebitoDTO.Numero,
                        titular: finalizarPagamentoDTO.CartaoDebitoDTO.Titular,
                        saldo: finalizarPagamentoDTO.CartaoDebitoDTO.Saldo,
                        validade: finalizarPagamentoDTO.CartaoDebitoDTO.Validade)) ;
                    if (pagamentoDebito is null) return BadRequest();
                    return Ok(_pedidoService.Pagamento(id, pagamentoDebito));

                case EFormaPagamento.Pix:
                    finalizarPagamentoDTO.PixDTO.Validar();
                    var pagamentoPix = new FinalizarPagamento(
                        formaPagamento: finalizarPagamentoDTO.FormaPagamento,
                        valor: valor,
                        pix: new Pix(agencia:finalizarPagamentoDTO.PixDTO.Agencia,
                        nomeTitular:finalizarPagamentoDTO.PixDTO.NomeTitular,
                        numeroConta:finalizarPagamentoDTO.PixDTO.NumeroConta,
                        instituicao:finalizarPagamentoDTO.PixDTO.Instituicao,
                        tipoChave:finalizarPagamentoDTO.PixDTO.TipoChave));
                    if (pagamentoPix is null) return BadRequest();
                    return Ok(_pedidoService.Pagamento(id, pagamentoPix));
            }
            return BadRequest();
        }
    }
}
