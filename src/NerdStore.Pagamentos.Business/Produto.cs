namespace NerdStore.Pagamentos.Business
{
    public class Produto //não será persistida, apenas uma simulação para esse contexto
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}