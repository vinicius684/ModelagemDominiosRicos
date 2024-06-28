using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoAtualizadoEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid PedidoId { get; private set; }
        public decimal ValorTotal { get; private set; }

        public PedidoAtualizadoEvent(Guid clientId, Guid pedidoId, decimal valorTotal)
        {
            AggregateId = pedidoId;
            ClientId = clientId;
            PedidoId = pedidoId;
            ValorTotal = valorTotal;
        }
    }
}