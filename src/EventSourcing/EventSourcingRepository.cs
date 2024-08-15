using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventStore.Client;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;
using Newtonsoft.Json;

namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository //Obs: não implementou o IDisposable, porque não vai fechar a conexão, assim como pedido pela documentação
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            // Cria a coleção de EventData
            var eventData = FormatarEvento(evento);

            // Append o evento ao stream
            await _eventStoreService.GetClient().AppendToStreamAsync(
                evento.AggregateId.ToString(), // Nome do stream
                StreamState.Any, // Indica que qualquer versão do stream é aceitável
                eventData, // Eventos a serem adicionados
                cancellationToken: default); // Pode ser ajustado conforme necessário
        }

        public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId)
        {
            // Lê eventos do stream
            var result = _eventStoreService.GetClient().ReadStreamAsync(
                Direction.Forwards,
                aggregateId.ToString(), // Nome do stream
                StreamPosition.Start, // Início do stream
                cancellationToken: default); // Pode ser ajustado conforme necessário

            var listaEventos = new List<StoredEvent>();

            // Itera sobre os eventos lidos
            await foreach (var resolvedEvent in result)
            {
                // Converte ReadOnlyMemory<byte> para string Json
                var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span);

                // Desserializa o JSON
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);

                var evento = new StoredEvent(
                    resolvedEvent.Event.EventId.ToGuid(), // Converte UUID para Guid
                    resolvedEvent.Event.EventType,
                    jsonData.Timestamp,
                    dataEncoded);

                listaEventos.Add(evento);
            }

            // Ordena e retorna os eventos
            return listaEventos.OrderBy(e => e.DataOcorrencia);
        }

        private static IEnumerable<EventData> FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            var eventDataJson = JsonConvert.SerializeObject(evento);
            var eventDataBytes = Encoding.UTF8.GetBytes(eventDataJson);
            // Cria o EventData com o novo formato do pacote EventStore.Client.Grpc.Streams
            yield return new EventData(
                Uuid.NewUuid(), // Gera um novo UUID para o evento
                evento.MessageType, // Tipo de evento (por exemplo, "OrderPlaced")

                eventDataBytes // Serializa o evento para bytes UTF-8
            );
        }
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}