using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Enums;

namespace PersonalFinance.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Transaction>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<decimal> SumByTypeAsync(TransactionType transactionType, CancellationToken cancellationToken = default);
}
