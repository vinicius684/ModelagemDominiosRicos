using MediatR;

namespace NerdStore.Core.Messages
{
    public abstract class Event : Message, INotification//classe base
    { 
        public DateTime Timestamp { get; private set;}

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
