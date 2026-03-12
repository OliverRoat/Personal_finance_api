using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.DTOs.Summaries;
using PersonalFinance.Application.Interfaces.Services;

namespace PersonalFinance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SummariesController : ControllerBase
{
    private readonly ISummaryService _summaryService;

    public SummariesController(ISummaryService summaryService)
    {
        _summaryService = summaryService;
    }

    [HttpGet("financial")]
    public async Task<ActionResult<FinancialSummaryDto>> GetFinancialSummary(CancellationToken cancellationToken)
    {
        var summary = await _summaryService.GetFinancialSummaryAsync(cancellationToken);
        return Ok(summary);
    }
}
