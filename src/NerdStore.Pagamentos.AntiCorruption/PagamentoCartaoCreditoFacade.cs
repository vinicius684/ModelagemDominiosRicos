using NerdStore.Pagamentos.Business;

namespace NerdStore.Pagamentos.AntiCorruption
{
    public class PagamentoCartaoCreditoFacade : IPagamentoCartaoCreditoFacade //Facade implementando a interface da camada de business
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configManager;

        public PagamentoCartaoCreditoFacade(IPayPalGateway payPalGateway, IConfigurationManager configManager)
        {
            _payPalGateway = payPalGateway;
            _configManager = configManager;
        }

        public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento)
        {
            var apiKey = _configManager.GetValue("apiKey");
            var encriptionKey = _configManager.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, pagamento.NumeroCartao);

            var pagamentoResult = _payPalGateway.CommitTransaction(cardHashKey, pedido.Id.ToString(), pagamento.Valor); //No mundo real aqui passaria mais informações e não receberia um bool, receberia um objeto do qual vc transformaria esse objeto numa transação

            // TODO: O gateway de pagamentos que deve retornar o objeto transação - Toda essa parte deveria ser feita pelo método commitTransaction. No máximo aqui, eu tranformaria a transacao retornada pelo paypal em transacao do meu domínio aqui, por seu uma facade
            var transacao = new Transacao //simulacao de obj retornado pelo paypal
            {
                PedidoId = pedido.Id,
                Total = pedido.Valor,
                PagamentoId = pagamento.Id
            };

            if (pagamentoResult)//Dependendo do resultado do pagamento vou setar o StatusTanscacao como pago ou não
            {
                transacao.StatusTransacao = StatusTransacao.Pago;
                return transacao;
            }

            transacao.StatusTransacao = StatusTransacao.Recusado;
            return transacao;
        }
    }
}