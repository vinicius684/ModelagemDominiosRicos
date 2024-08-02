using System;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class PedidoPagamentoRecusadoEvent : IntegrationEvent //Se eu tivesse utilizando um banco de leitura, eu teria que realizarr o registro da transação e do pagamento. Através desse evento, eu obteria essas infos para registrar no meu banco de leitura
{
    public Guid PedidoId { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid PagamentoId { get; private set; }
    public Guid TransacaoId { get; private set; }
    public decimal Total { get; private set; }

    public PedidoPagamentoRecusadoEvent(Guid pedidoId, Guid clienteId, Guid pagamentoId, Guid transacaoId, decimal total)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        ClienteId = clienteId;
        PagamentoId = pagamentoId;
        TransacaoId = transacaoId;
        Total = total;
    }
}