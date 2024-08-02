using MediatR;
using NerdStore.Core.Communiation.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Events
{
    // Classe de manipulação
    public class ProdutoEventHandler :
        INotificationHandler<ProdutoAbaixoEstoqueEvent>,
        INotificationHandler<PedidoIniciadoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMediatrHandler _mediatrHandler;

        public ProdutoEventHandler(IProdutoRepository produtoRepository, IEstoqueService estoqueService, IMediatrHandler mediatrHandler) 
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
            _mediatrHandler = mediatrHandler;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken) //Método da interface
        {
            var produto = _produtoRepository.ObterPorId(mensagem.AggregateId);

            //Enviar um email para aquisição de mais produtos
        }

        public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)//Evento de integração
        {
            var result = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);

            if (result)
            {
                await _mediatrHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(message.PedidoId, message.ClienteId, message.Total, message.ProdutosPedido, message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao));// sinal verde p realizar pagamento
            }
            else
            {
                await _mediatrHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(message.PedidoId, message.ClienteId));//Cancelar o pedido ou retornar mensagem de erro que estoque nãot a mais disponível
            }
        }
    }
}
