using System;
using System.Linq;

namespace NerdStore.Pagamentos.AntiCorruption
{
    public class ConfigurationManager : IConfigurationManager //Simula uma Classe aux que vai obter dados de configuração
    {
        public string GetValue(string node)//esse valor pode estar dentro de um arquivo de configuração, qualquer outro lugar(nó de configuração)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)//retornando uma string com caracteres aleatórios da sequencia passada(simulação do node)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}