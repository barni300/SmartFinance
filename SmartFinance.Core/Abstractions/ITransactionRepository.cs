using SmartFinance.Core.Models;

namespace SmartFinance.Core.Abstractions;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync();
    Task<Transaction> AddAsync(Transaction tx);
    Task DeleteAsync(int id);
}