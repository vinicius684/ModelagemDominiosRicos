namespace NerdStore.Pagamentos.Business
{
    public interface IPagamentoCartaoCreditoFacade //essa Ifacade está aqui pq não quero dependencia da anticorruption na minha business
    {
        Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento);
    }
}