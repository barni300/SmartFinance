using SmartFinance.Core.Models;

namespace SmartFinance.Application.DTO;

public sealed class CreateTransactionRequest
{
    public DateTime Date { get; init; }
    public string AmountText { get; init; } = "";
    public string Category { get; init; } = "";
    public string? Note { get; init; }
    public TransactionType Type { get; init; } = TransactionType.Expense;
}