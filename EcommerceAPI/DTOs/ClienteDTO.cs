using EcommerceAPI.Entidades;
using EcommerceAPI.Enumeradores;

namespace EcommerceAPI.DTOs
{
    public class ClienteDTO : Validator
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Documento { get; set; }
        public ETipoPessoa TipoPessoa { get; set; }
        public PedidoDTO? PedidoDTO { get; set; }

        public override void Validar()
        {
            Valido = true;
            if(string.IsNullOrEmpty(Nome) || Nome.Length > 100 || Sobrenome.Length > 50)
                Valido = false;
        }
    }
}
