namespace NerdStore.Core.Data
{
    public interface IUnitOfWork//define um contrato para controlar transações em um contexto de BD, Commit() é usado para confirmar todas as operações realizadas em uma única transação
    {
        Task<bool> Commit();
    }
}
