using System;

namespace EcommerceAPI.DTOs
{
    public class CartaoDebitoDTO : Validator
    {
        public string CVV { get;set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Validade { get; set; }
        public override void Validar()
        {
            Valido = true;
            if (CVV.Length != 3)
                Valido = false;

            if (Numero.Length != 9)
                Valido = false;
            
            if(Saldo < 0)
                Valido = false;

            if (DateTime.Now >= Validade)
                Valido = false;
        }
    }
}
