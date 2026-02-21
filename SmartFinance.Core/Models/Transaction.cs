namespace SmartFinance.Core.Models;

public enum TransactionType
{
    Income = 1,
    Expense = 2
}

public class Transaction
{
    public int Id { get; set; }

    // Дата операции
    public DateTime Date { get; set; } = DateTime.Today;

    // Сумма (всегда положительная), знак определяет Type
    public decimal Amount { get; set; }

    // Категория (Food, Rent, Crypto...)
    public string Category { get; set; } = "";

    // Комментарий
    public string? Note { get; set; }

    public TransactionType Type { get; set; } = TransactionType.Expense;
}