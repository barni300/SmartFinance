using Microsoft.EntityFrameworkCore;
using SmartFinance.Core.Models;

namespace SmartFinance.Data.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transaction>()
            .Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(64);

        modelBuilder.Entity<Transaction>()
            .Property(x => x.Amount)
            .HasPrecision(18, 2);
    }
}