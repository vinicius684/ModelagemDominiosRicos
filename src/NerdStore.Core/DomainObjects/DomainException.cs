using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException()//Construtor da qual simplesmente cria uma instancia
        { }

        public DomainException(string message) : base(message)//outro quando passa uma mensagem especializada
        { }

        public DomainException(string message, Exception innerException) : base(message, innerException)//mensagem erro da regra de negócio e exception mais especializada Ex: Erro no calculo, Erro divisão por 0
        { }
    }
}
