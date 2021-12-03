using System;

namespace EcommerceAPI.Entidades.Pagamento
{
    public class CartaoDebito
    {
        
        public CartaoDebito(
            string cVV,
            string numero,
            string titular,
            decimal saldo,
            DateTime validade)
        {
            CVV = cVV;
            Numero = numero;
            Titular = titular;
            Saldo = saldo;
            Validade = validade;
        }

        public string CVV { get; private set; }
        public string Numero { get; private set; }
        public string Titular { get; private set; }
        public decimal Saldo { get; private set; }
        public DateTime Validade { get; private set; }
    }
}

