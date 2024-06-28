using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoItemAdicionadoEvent : Event 
    {
        public Guid ClientId { get; private set; }
        public Guid PedidoId { get; private set;}
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public decimal ValorUnitario { get; set; }
        public int Quantida { get; private set; }

        public PedidoItemAdicionadoEvent(Guid clientId, Guid pedidoId, Guid produtoId, string produtoNome, decimal valorUnitario, int quantida)
        {
            AggregateId = pedidoId;
            ClientId = clientId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            ValorUnitario = valorUnitario;
            Quantida = quantida;
        }
    }
}