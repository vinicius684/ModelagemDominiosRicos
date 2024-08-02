using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Pagamentos.Business
{
    //pagamento é do meu negócio, já a transação é o resultado do gatway de pagamento
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid PedidoId { get; set; }
        public string Status { get; set; }
        public decimal Valor { get; set; }

        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; } //posso fazer um tratamento p guardar só os 4 primeiros e 2 últimos digitos do cartão
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }

        // EF. Rel.
        public Transacao Transacao { get; set; }
    }
}
