using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.Communiation.Mediator
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;//Publicar Evento, Ex: EstoqueService

        Task<bool> EnviarComando<T>(T evento) where T : Command;//Enviar Comando, Ex: AddItemPedidoCommand

        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;//INotification, assim como o Event, porém com outra classe base criada pois tem Atributos diferentes

    }
}
