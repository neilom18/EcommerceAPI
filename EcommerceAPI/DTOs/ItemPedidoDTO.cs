namespace EcommerceAPI.DTOs
{
    public class ItemPedidoDTO : Validator
    {
        public ProdutoDTO ProdutoDTO{ get; set; }
        public int Quantidade { get;private set; } = 1;

        public override void Validar()
        {
            Valido = true;
            if(Quantidade <= 0) Valido = false;
            if(ProdutoDTO.Preco <= 0) Valido=false;
        }
        public void SetQuantidade(int n)
        {
            if(n <= 0) Valido = false;
            ProdutoDTO.Validar();
            if(ProdutoDTO.Valido == false) Valido = false;
            if(ProdutoDTO is null) Valido = false;
            if(Valido) Quantidade = n;
        }
    }
}
