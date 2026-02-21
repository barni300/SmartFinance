using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using SmartFinance.Core.Abstractions;
using SmartFinance.Data.Persistence;
using SmartFinance.Data.Repositories;
using SmartFinance.ViewModels;
using SmartFinance.Application.Services;

namespace SmartFinance;

public partial class App : System.Windows.Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // 1️⃣ Формируем путь к SQLite файлу
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SmartFinance",
            "smartfinance.db"
        );

        // 2️⃣ Создаём папку, если её нет
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        // 3️⃣ Настраиваем EF Core
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        var db = new AppDbContext(options);

        // 4️⃣ Создаём базу, если её ещё нет
        await db.Database.EnsureCreatedAsync();

        // 5️⃣ Создаём репозиторий
        ITransactionRepository repo = new TransactionRepository(db);
        var service = new TransactionService(repo);
        var vm = new MainViewModel(service);
        // 6️⃣ Создаём ViewModel и передаём туда repo
        
        await vm.LoadAsync();

        // 7️⃣ Создаём и показываем окно
        var window = new MainWindow
        {
            DataContext = vm
        };

        window.Show();
    }
}