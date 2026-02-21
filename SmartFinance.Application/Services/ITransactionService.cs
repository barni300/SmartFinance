using SmartFinance.Application.DTO;
using SmartFinance.Core.Models;

namespace SmartFinance.Application.Services;

public interface ITransactionService
{
    Task<List<Transaction>> GetAllAsync();
    Task<(bool ok, string? error, Transaction? tx)> CreateAsync(CreateTransactionRequest req);
    Task DeleteAsync(int id);
}