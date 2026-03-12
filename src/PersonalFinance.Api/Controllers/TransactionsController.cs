using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.DTOs.Transactions;
using PersonalFinance.Application.Interfaces.Services;

namespace PersonalFinance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponseDto>> CreateTransaction(
        [FromBody] TransactionCreateDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var created = await _transactionService.CreateAsync(request, cancellationToken);
            return Created($"/api/transactions/{created.Id}", created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TransactionResponseDto>>> GetTransactions(
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionService.GetAllAsync(cancellationToken);
        return Ok(transactions);
    }
}
