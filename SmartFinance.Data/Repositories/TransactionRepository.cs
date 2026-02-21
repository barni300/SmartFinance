using Microsoft.EntityFrameworkCore;
using SmartFinance.Core.Abstractions;
using SmartFinance.Core.Models;
using SmartFinance.Data.Persistence;

namespace SmartFinance.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _db;

    public TransactionRepository(AppDbContext db) => _db = db;

    public async Task<List<Transaction>> GetAllAsync()
        => await _db.Transactions
            .OrderByDescending(t => t.Date)
            .ThenByDescending(t => t.Id)
            .ToListAsync();

    public async Task<Transaction> AddAsync(Transaction tx)
    {
        _db.Transactions.Add(tx);
        await _db.SaveChangesAsync();
        return tx;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Transactions.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) return;

        _db.Transactions.Remove(entity);
        await _db.SaveChangesAsync();
    }
}