using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartFinance.Application.DTO;
using SmartFinance.Application.Services;
using SmartFinance.Core.Models;

namespace SmartFinance.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ITransactionService _service;

    public ObservableCollection<Transaction> Items { get; } = new();

    [ObservableProperty] private DateTime date = DateTime.Today;
    [ObservableProperty] private string amountText = "";
    [ObservableProperty] private string category = "Food";
    [ObservableProperty] private string? note;

    public TransactionType[] Types { get; } =
        (TransactionType[])Enum.GetValues(typeof(TransactionType));

    [ObservableProperty] private TransactionType selectedType = TransactionType.Expense;

    [ObservableProperty] private Transaction? selectedTransaction;

    public MainViewModel(ITransactionService service)
    {
        _service = service;
    }

    [RelayCommand]
    public async Task LoadAsync()
    {
        var all = await _service.GetAllAsync();
        Items.Clear();
        foreach (var t in all) Items.Add(t);
    }

    [RelayCommand]
    public async Task AddAsync()
    {
        var req = new CreateTransactionRequest
        {
            Date = Date,
            AmountText = AmountText,
            Category = Category,
            Note = Note,
            Type = SelectedType
        };

        var (ok, error, tx) = await _service.CreateAsync(req);
        if (!ok || tx is null)
        {
            // пока просто молча, позже сделаем вывод ошибки в UI
            return;
        }

        Items.Insert(0, tx);

        AmountText = "";
        Note = null;
    }

    [RelayCommand(CanExecute = nameof(CanDelete))]
    public async Task DeleteSelectedAsync()
    {
        if (SelectedTransaction is null) return;

        await _service.DeleteAsync(SelectedTransaction.Id);
        Items.Remove(SelectedTransaction);
        SelectedTransaction = null;
    }

    private bool CanDelete() => SelectedTransaction is not null;

    partial void OnSelectedTransactionChanged(Transaction? value)
        => DeleteSelectedCommand.NotifyCanExecuteChanged();
}