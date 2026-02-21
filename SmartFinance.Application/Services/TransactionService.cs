using System.Globalization;
using SmartFinance.Application.DTO;
using SmartFinance.Core.Abstractions;
using SmartFinance.Core.Models;

namespace SmartFinance.Application.Services;

public sealed class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;

    public TransactionService(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Transaction>> GetAllAsync() => _repo.GetAllAsync();

    public async Task<(bool ok, string? error, Transaction? tx)> CreateAsync(CreateTransactionRequest req)
    {
        // 1) валидация
        if (string.IsNullOrWhiteSpace(req.Category))
            return (false, "Category is required.", null);

        var raw = (req.AmountText ?? "").Trim();
        if (string.IsNullOrWhiteSpace(raw))
            return (false, "Amount is required.", null);

        // 2) парсинг суммы: разрешаем 12,5 и 12.5
        raw = raw.Replace(',', '.');

        if (!decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out var amount))
            return (false, "Amount format is invalid.", null);

        if (amount <= 0)
            return (false, "Amount must be > 0.", null);

        // 3) создаём доменную сущность
        var tx = new Transaction
        {
            Date = req.Date.Date,
            Amount = amount,
            Category = req.Category.Trim(),
            Note = string.IsNullOrWhiteSpace(req.Note) ? null : req.Note.Trim(),
            Type = req.Type
        };

        // 4) сохраняем
        await _repo.AddAsync(tx);
        return (true, null, tx);
    }

    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
}