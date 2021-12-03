using System;

namespace EcommerceAPI.Entidades.Pagamento
{
    public class CartaoCredito
    {
        public CartaoCredito(
            string cVV,
            string numero,
            string titular,
            decimal limite,
            DateTime validade)
        {
            CVV = cVV;
            Numero = numero;
            Titular = titular;
            Limite = limite;
            Validade = validade;
        }

        public string CVV { get; private set; }
        public string Numero { get; private set; }
        public string Titular { get; private set; }
        public decimal Limite { get; private set; }
        public DateTime Validade { get; private set; }

    }
}
