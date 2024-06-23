using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotification : Message, INotification //Replica a estrutura do event ao inves de Herda-ló, pois não quero que seja tratada como uma raíz de evento
    {
        public DateTime Timestamp { get; private set; }
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            Timestamp = DateTime.Now;
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
