using EcommerceAPI.DTOs;
using EcommerceAPI.Entidades;
using EcommerceAPI.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EcommerceAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clienteService.Get());
        }
        [HttpGet, Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_clienteService.Get(id));
        }
        [HttpPost]
        public IActionResult Post(ClienteDTO clienteDTO)
        {
            clienteDTO.Validar();

            if (!clienteDTO.Valido) return BadRequest("Informações do Cliente Invalidas!!");
            var guid = Guid.NewGuid();
            var client = new Cliente(
                id: guid,
                nome: clienteDTO.Nome,
                sobrenome: clienteDTO.Sobrenome,
                documento: clienteDTO.Documento,
                tipoPessoa: clienteDTO.TipoPessoa
                );

            return Created("", _clienteService.Cadastrar(client));
        }
        [HttpPut, Route("{id}")]
        public IActionResult Put(Guid id,ClienteDTO clienteDTO)
        {
            clienteDTO.Validar();

            if (!clienteDTO.Valido) return BadRequest();

            var client = new Cliente(
                id: id,
                nome: clienteDTO.Nome,
                sobrenome: clienteDTO.Sobrenome,
                documento: clienteDTO.Documento,
                tipoPessoa: clienteDTO.TipoPessoa);

            return Created("", _clienteService.Atualizar(id, client));
        }
        [HttpDelete, Route("{id}")]
        public IActionResult Delete(Guid id) 
        {
            if (_clienteService.Deletar(id)) return Ok() ;
            return NotFound();
        }
    }
}
