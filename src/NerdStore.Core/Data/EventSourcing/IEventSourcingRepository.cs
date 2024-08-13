using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event; //recebo um evento genérico que deve ser filho de Event
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId); //receber um coleção de StoredEvent, que vou obte-los através do aggregateId
    }
}