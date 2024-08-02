using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoEventHandler :
        //INotificationHandler<PedidoRascunhoIniciadoEvent>,
        //INotificationHandler<PedidoItemAdicionadoEvent>,
        INotificationHandler<PedidoEstoqueRejeitadoEvent>
    {
        //public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        //{
        //    return Task.CompletedTask;
        //}

        //public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        //{
        //    return Task.CompletedTask;
        //}

        public Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
        {
            //cancelar o processamento do pedido - retornar erro para o cliente
            return Task.CompletedTask;
        }
    }
}
