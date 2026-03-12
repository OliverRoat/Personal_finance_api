using System.ComponentModel.DataAnnotations;
using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Application.DTOs.Transactions;

public class TransactionCreateDto
{
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    [EnumDataType(typeof(TransactionType))]
    public TransactionType TransactionType { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}
