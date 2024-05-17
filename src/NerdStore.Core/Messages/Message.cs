using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Message
{
    public abstract class Message
    {
        public string MessageType { get; protected set;}//Tipo da Menssagem

        public Guid AggregateId { get; protected set;}//Sempre me referindo a algum agregado naquela mensagem, logo o id dele

        protected Message() 
        {
            MessageType = GetType().Name;//retornando nome da classe que está implementando a classe Message
        }
    }
}
