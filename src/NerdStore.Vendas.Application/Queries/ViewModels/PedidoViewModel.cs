using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Queries.ViewModels
{
    public class PedidoViewModel
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }//Ou DataRealizacao
        public int PedidoStatus { get; set; }
    }
}
