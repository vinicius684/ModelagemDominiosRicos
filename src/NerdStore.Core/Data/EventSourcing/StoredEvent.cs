using System;

namespace NerdStore.Core.Data.EventSourcing
{
    public class StoredEvent //Representação do evento recuperado do EventStoreDb na minha app
    {
        public StoredEvent(Guid id, string tipo, DateTime dataOcorrencia, string dados)
        {
            Id = id;
            Tipo = tipo;
            DataOcorrencia = dataOcorrencia;
            Dados = dados;
        }

        public Guid Id { get; private set; } //id do evento

        public string Tipo { get; private set; } //tipo - o que aconteceu... Ex: "PedidoItemAdicionado"

        public DateTime DataOcorrencia { get; set; }

        public string Dados { get; private set; } //serealização em json do evento inteiro. Pra quando eu recuperar deserealizar e conseguir tranformar na minha classe de evento do "Atributo TIpo"
    }
}