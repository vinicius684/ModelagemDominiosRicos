using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;//Publicar Evento, Ex: EstoqueService

        Task<bool> EnviarComando<T>(T evento) where T : Command;//Enviar Comando, Ex: AddItemPedidoCommand

    }
}
