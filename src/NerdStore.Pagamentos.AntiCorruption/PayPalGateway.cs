using System;
using System.Linq;

namespace NerdStore.Pagamentos.AntiCorruption
{
    public class PayPalGateway : IPayPalGateway
    {
        public bool  CommitTransaction(string cardHashKey, string orderId, decimal amount) //3passa o cardHashKey gerado para realizar um pagamento com aql cartão, id do pedido e total 
        {
            return new Random().Next(2) == 0;//sortear de 0 a 2, se for zero retorna true. Ou seja, operação aprovada no cartão vai ser sorteio, apenas Demo
            //return false;
        }

        public string GetCardHashKey(string serviceKey, string nCartaoCredito)//2 Com a chave gerada anteriormente no GetPayPalServiceKey e dados do cartão de crédito, vou gerar o GetCardHashKey
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string GetPayPalServiceKey(string apiKey, string encriptionKey) //1 obtendo chave de serviço do paypal,apiKey(segredo da sua api no Registro do paypal) e EncriptionKey
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}