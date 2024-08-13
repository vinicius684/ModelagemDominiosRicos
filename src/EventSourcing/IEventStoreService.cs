using EventStore.ClientAPI;

namespace EventSourcing
{
    public interface IEventStoreService //meio de comunicação com meu banco 
    {
        IEventStoreConnection GetConnection();
    }
}