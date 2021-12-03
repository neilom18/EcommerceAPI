using System.Collections.Generic;

namespace EcommerceAPI.DTOs
{
    public class PedidoDTO : Validator
    {
        public List<ItemPedidoDTO>? ItemPedidoDTO { get; set; }
        public ClienteDTO Cliente { get; set; }
        public decimal Preco { get;private set; }

        public override void Validar()
        {
            
        }
    }
}
