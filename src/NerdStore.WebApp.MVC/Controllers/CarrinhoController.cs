using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Communiation.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IMediatrHandler _mediatrHandler;


        public CarrinhoController(INotificationHandler<DomainNotification> notifications, IProdutoAppService produtoAppService, IMediatrHandler mediatrHandler) : base(notifications, mediatrHandler)
        {
            _produtoAppService = produtoAppService;
            _mediatrHandler = mediatrHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);//ClienteId é o cliente logado, mas esse não é foco desse curso, portanto será criada uma classe para simular isso ControllerBase
            await _mediatrHandler.EnviarComando(command);

            //se tudo deu certo
            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

           TempData["Erros"] = ObterMensagensErro();//usando tempData pois estou retornando um Redirect, que perde o estado do request anterior;
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }
    }
}
