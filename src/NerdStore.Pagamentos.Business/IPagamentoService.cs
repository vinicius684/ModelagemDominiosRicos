using System.Threading.Tasks;
using NerdStore.Core.DomainObjects.DTO;

namespace NerdStore.Pagamentos.Business
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido); //Esse parâmetro trata-se de uma DTO em Core
    }
}