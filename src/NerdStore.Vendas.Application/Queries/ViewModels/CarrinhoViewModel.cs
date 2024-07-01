namespace NerdStore.Vendas.Application.Queries.ViewModels
{
    public class CarrinhoViewModel //agraga quase todas as infos
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public string VoucherCodigo { get; set; }

        public List<CarrinhoItemViewModel> Items { get; set; } = new List<CarrinhoItemViewModel>();
        public CarrinhoPagamentoViewModel Pagamento { get; set; }
    }
}
