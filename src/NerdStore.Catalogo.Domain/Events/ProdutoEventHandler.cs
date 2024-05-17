using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Events
{
    // Classe de manipulação
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent> 
    {
        private readonly IProdutoRepository _produtoRepository;

        ProdutoEventHandler(IProdutoRepository produtoRepository) 
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken) //Método da interface
        {
            var produto = _produtoRepository.ObterPorId(mensagem.AggregateId);

            //Enviar um email para aquisição de mais produtos
        }
    }
}
