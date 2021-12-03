using System;
using EcommerceAPI.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Servicos
{
    public class ClienteService
    {
        private readonly List<Cliente> _cliente;
        private readonly PedidoService _pedidoService;

        public ClienteService(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
            _cliente ??= new List<Cliente>();
        }
        public Cliente Cadastrar(Cliente cliente)
        {   
            _cliente.Add(cliente);
            _pedidoService.Adicionar(cliente.Pedido);
            return cliente;
        }
        public IEnumerable<Cliente> Get()
        {
            return _cliente;
        }
        public Cliente Get(Guid id)
        {
            return _cliente.Where(p => p.Id == id).SingleOrDefault();
        }
        public Cliente Atualizar(Guid id, Cliente cliente)
        {
            var client = _cliente.Where(p => p.Id == id).SingleOrDefault();

            client.Atualizar(cliente);

            return client;
        }
        public bool Deletar(Guid id)
        {
            var client = _cliente.Where(p => p.Id == id).SingleOrDefault();

            if (client == null) return false;
            
            if(_cliente.Remove(client)) return true;
            
            return false;
        }
    }
}
