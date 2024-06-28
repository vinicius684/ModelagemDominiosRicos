using NerdStore.Core.Communiation.Mediator;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Data
{
    public static class MediatrExtension
    {
        public static async Task PublicarEventos(this IMediatrHandler mediator, VendasContext ctx) //método Padrão
        {
            //pegar todas as entidades modificadas que estiverem no meu ChangeTracker, das quais as entradas são do tipo Entity e onde possuem alguma notificação
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            //selecionar todos os eventos de domínio
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            //tranformar numa lista e limpar em seguida
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            //task onde meu evetnos ao serem selecionados na lista vão ser publicados um a um
            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublicarEvento(domainEvent);
                });

            //voltar quando todos meus eventos forem lançados
            await Task.WhenAll(tasks);
        }
    }
}
