using EventStore.Client;

namespace EventSourcing
{
    public interface IEventStoreService//meio de comunicação com meu banco
    {
        EventStoreClient GetClient();
    }
}