using MediatR;
using NerdStore.Core.Communiation.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands;
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
        INotificationHandler<PedidoEstoqueRejeitadoEvent>,
        INotificationHandler<PedidoPagamentoRealizadoEvent>,
        INotificationHandler<PedidoPagamentoRecusadoEvent>

    {
        private readonly IMediatrHandler _mediatorHandler;

        public PedidoEventHandler(IMediatrHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }



        //public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        //{
        //    return Task.CompletedTask;
        //}

        //public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        //{
        //    return Task.CompletedTask;
        //}

        public async Task Handle(PedidoEstoqueRejeitadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PedidoPagamentoRealizadoEvent message, CancellationToken cancellationToken) //finalizar pedido.- Mundo Real a partir disso Lançar evento PedidoFinalizado para Emissão de nota fiscal e depois um evento para envia-la para o email, Logistica etc
        {
            await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(message.PedidoId, message.ClienteId));
        }

        public async Task Handle(PedidoPagamentoRecusadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoEstornarEstoqueCommand(message.PedidoId, message.ClienteId));
        }

    }
}
