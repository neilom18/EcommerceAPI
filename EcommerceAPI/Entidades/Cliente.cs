using EcommerceAPI.Enumeradores;
using System;

namespace EcommerceAPI.Entidades
{
    public class Cliente : EntidadeBase
    {
        public Cliente(Guid id,string nome, string sobrenome, string documento, ETipoPessoa tipoPessoa) : base(id)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            TipoPessoa = tipoPessoa;
            Pedido = new Pedido(this);
        }

        public string Nome { get;private set; }
        public string Sobrenome { get;private set; }
        public string Documento { get;private set; }
        public ETipoPessoa TipoPessoa { get;private set; }
        public Pedido? Pedido { get;private set; }
        public void Atualizar(Cliente cliente)
        {
            Nome = cliente.Nome;
            Sobrenome = cliente.Sobrenome;
            Documento =cliente.Documento;
            TipoPessoa = cliente.TipoPessoa;
        }
    }
}
