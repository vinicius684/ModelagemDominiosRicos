using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Communiation.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatrHandler _mediatrHandler;

        public EstoqueService(IProdutoRepository produtoRepository,
                              IMediatrHandler mediatrHandler)
        {
            _produtoRepository = produtoRepository;
            _mediatrHandler = mediatrHandler;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await DebitarItemEstoque(produtoId, quantidade)) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await _mediatrHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));
                return false;
            }

            produto.DebitarEstoque(quantidade);

            // TODO: 10 pode ser parametrizavel em arquivo de configuração
            if (produto.QuantidadeEstoque < 10)
            {
                // avisar, mandar email, abrir chamado, realizar nova compra. Coisas esssas que não devem ser feitas aqui, estaria corrompendo a responsabilidade do método. Aí que entra o handle desse evento de domínio
                await _mediatrHandler.PublicarDomainEvent(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return true;
        }

        //public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        //{
        //    var produto = await _produtoRepository.ObterPorId(produtoId);

        //    if (produto == null) return false;

        //    if (!produto.PossuiEstoque(quantidade)) return false;

        //    produto.DebitarEstoque(quantidade);

        //    // TODO: Parametrizar a quantidade de estoque baixo
        //    if (produto.QuantidadeEstoque < 10)
        //    {
        //     
        //        await _bus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
        //    }

        //    _produtoRepository.Atualizar(produto);
        //    return await _produtoRepository.UnitOfWork.Commit();
        //}

        public async Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                await ReporItemEstoque(item.Id, item.Quantidade);
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var sucesso = await ReporItemEstoque(produtoId, quantidade);

            if (!sucesso) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return true;
        }

        //public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        //{
        //    var produto = await _produtoRepository.ObterPorId(produtoId);

        //    if (produto == null) return false;
        //    produto.ReporEstoque(quantidade);

        //    _produtoRepository.Atualizar(produto);
        //    return await _produtoRepository.UnitOfWork.Commit();
        //}

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
