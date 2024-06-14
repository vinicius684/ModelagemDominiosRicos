using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Bus;
using NerdStore.Vendas.Application.Commands;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IMediatrHandler _mediatorHandler;

        public CarrinhoController(IProdutoAppService produtoAppService, IMediatrHandler mediatrHandler)
        {
            _produtoAppService = produtoAppService;
            _mediatorHandler = mediatrHandler;
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
            await _mediatorHandler.EnviarComando(command);

            //se tudo deu certo
            //if (OperacaoValida())
            //{
            //    return RedirectToAction("Index");
            //}

            TempData["Erros"] = "Pr oduto Indisponível";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }
    }
}
