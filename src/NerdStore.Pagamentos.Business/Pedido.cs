using System;
using System.Collections.Generic;

namespace NerdStore.Pagamentos.Business
{
    public class Pedido //não será persistida, apenas uma simulação para esse contexto
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}