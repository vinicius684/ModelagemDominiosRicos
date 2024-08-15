 using EventStore.Client;
using Microsoft.Extensions.Configuration;

namespace EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly EventStoreClient _client;

        public EventStoreService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EventStoreConnection");

            var settings = EventStoreClientSettings.Create(connectionString);

            _client = new EventStoreClient(settings);
        }

        public EventStoreClient GetClient()
        {
            return _client;
        }
    }
}