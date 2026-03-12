using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Amount)
                .HasPrecision(18, 2);
            entity.Property(t => t.Description)
                .HasMaxLength(500)
                .IsRequired();
            entity.Property(t => t.TransactionType)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
            entity.Property(t => t.Date)
                .IsRequired();
            entity.HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Food" },
            new Category { Id = 2, Name = "Transportation" },
            new Category { Id = 3, Name = "Rent" },
            new Category { Id = 4, Name = "Salary" },
            new Category { Id = 5, Name = "Entertainment" }
        );

    }
}
