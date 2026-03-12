using PersonalFinance.Application.DTOs.Transactions;

namespace PersonalFinance.Application.Interfaces.Services;

public interface ITransactionService
{
    Task<TransactionResponseDto> CreateAsync(TransactionCreateDto request, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TransactionResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
