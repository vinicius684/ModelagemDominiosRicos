using System;
using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events
{
    //Serve pra dizer que um pedido foi iniciado como rascunho
    public class PedidoRascunhoIniciadoEvent : Event
    {
        //infos necessárias para o evento, se estivess por exemplo persistindo em um banco teria que colocar todas as propriedades que preciso pra isso
        public Guid ClienteId { get; private set; }
        public Guid PedidoId { get; private set; }

        public PedidoRascunhoIniciadoEvent(Guid clienteId, Guid pedidoId)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
        }
    }
}