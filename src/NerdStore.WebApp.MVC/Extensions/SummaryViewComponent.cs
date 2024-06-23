using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly DomainNotificationHandler _notifications;

        public SummaryViewComponent(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notifications.ObterNotificacoes());//buscar Notificações através do ObterNotificações
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Value));//ForEach nas notificações adicionando cada uma no ModelState, para que possa exibir atraves do modelState o retorno do formulário e exibir todas as notificações

            return View();
        }
    }
}