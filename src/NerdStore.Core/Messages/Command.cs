using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NerdStore.Core.Messages.CommonMessages.Notifications; //vai mediar a troca de menssagens através de encontrar o manipulador daquela mensagem através do recurso de injeção de dependência

namespace NerdStore.Core.Messages
{
    public abstract class Command : Message, IRequest<bool> //Request faz parte do MediatR e quer dizer que vai solicitar algo
    {
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }//Não é validationResult do DataAnnotations, é do fluent Validation. 

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
