using PersonalFinance.Application.DTOs.Summaries;

namespace PersonalFinance.Application.Interfaces.Services;

public interface ISummaryService
{
    Task<FinancialSummaryDto> GetFinancialSummaryAsync(CancellationToken cancellationToken = default);
}
