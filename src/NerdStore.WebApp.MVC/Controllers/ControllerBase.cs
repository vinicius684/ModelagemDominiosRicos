using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communiation.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;//Injetada classe pois ela implementa INotificationHandler do Mediatr, logo teria que injetar duas interfaces pra acessar todos os métodos
        private readonly IMediatrHandler _mediatrHandler;

        protected Guid ClienteId = Guid.Parse("bb32841f-4867-4165-9af2-d5f42841a4dc");

        //passando a interface da biblioteca MediatR(que não define todos os métodos) por design de código e fazendo casting dentro do construtor para acessar todos os métodos.
        protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                                 IMediatrHandler mediatrHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatrHandler = mediatrHandler;

        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        protected IEnumerable<string> ObterMensagensErro()
        { 
             return _notifications?.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatrHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }
    }
}
