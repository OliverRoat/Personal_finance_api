using PersonalFinance.Application.DTOs.Summaries;
using PersonalFinance.Application.Interfaces.Services;
using PersonalFinance.Domain.Enums;
using PersonalFinance.Infrastructure.Repositories.Interfaces;

namespace PersonalFinance.Infrastructure.Services;

public class SummaryService : ISummaryService
{
    private readonly ITransactionRepository _transactionRepository;

    public SummaryService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<FinancialSummaryDto> GetFinancialSummaryAsync(CancellationToken cancellationToken = default)
    {
        var totalIncome = await _transactionRepository.SumByTypeAsync(TransactionType.Income, cancellationToken);
        var totalExpense = await _transactionRepository.SumByTypeAsync(TransactionType.Expense, cancellationToken);

        return new FinancialSummaryDto
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            Balance = totalIncome - totalExpense
        };
    }
}
