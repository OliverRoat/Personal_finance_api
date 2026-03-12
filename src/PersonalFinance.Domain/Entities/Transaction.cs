using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TransactionType TransactionType { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
