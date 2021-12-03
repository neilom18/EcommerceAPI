using EcommerceAPI.DTOs;
using EcommerceAPI.Entidades;
using EcommerceAPI.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EcommerceAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_produtoService.Get());
        }
        [HttpGet, Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_produtoService.Get(id));
        }
        [HttpPost]
        public IActionResult Post(ProdutoDTO produtoDTO)
        {
            produtoDTO.Validar();

            if (!produtoDTO.Valido) return BadRequest("Produto informado invalido!!");
            var guid = Guid.NewGuid();
            var prod = new Produto(id: guid, nome: produtoDTO.Nome, descricao: produtoDTO.Descricao, preco: produtoDTO.Preco);

            return Created("", _produtoService.Cadastrar(prod));
        }
        [HttpPut, Route("{id}")]
        public IActionResult Put(Guid id, ProdutoDTO produtoDTO)
        {
            produtoDTO.Validar();

            if (!produtoDTO.Valido) return BadRequest("Produto informado invalido!!");
            var prod = new Produto(
                id: id,
                nome: produtoDTO.Nome,
                descricao: produtoDTO.Descricao,
                preco: produtoDTO.Preco);

            return Created("", _produtoService.Atualizar(id,prod));
        }
        [HttpDelete, Route("{id}")]
        public IActionResult Deletar(Guid id)
        {
            if (_produtoService.Deletar(id)) return Ok();
            return NoContent();
        }
    }
}
