using PersonalFinance.Application.DTOs.Transactions;
using PersonalFinance.Application.Interfaces.Services;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Infrastructure.Repositories.Interfaces;

namespace PersonalFinance.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;

    public TransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<TransactionResponseDto> CreateAsync(TransactionCreateDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new ArgumentException("Transaction description is required.");
        }

        if (request.Date == default)
        {
            throw new ArgumentException("A valid transaction date is required.");
        }

        var categoryName = await _categoryRepository.GetNameByIdAsync(request.CategoryId, cancellationToken);
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            throw new KeyNotFoundException($"Category with id {request.CategoryId} was not found.");
        }

        var transaction = new Transaction
        {
            Amount = request.Amount,
            Description = request.Description.Trim(),
            Date = request.Date,
            TransactionType = request.TransactionType,
            CategoryId = request.CategoryId
        };

        var created = await _transactionRepository.AddAsync(transaction, cancellationToken);
        return MapToResponse(created, categoryName);
    }

    public async Task<IReadOnlyCollection<TransactionResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);
        return transactions
            .Select(t => MapToResponse(t, t.Category?.Name ?? string.Empty))
            .ToList();
    }

    private static TransactionResponseDto MapToResponse(Transaction transaction, string categoryName)
    {
        return new TransactionResponseDto
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Description = transaction.Description,
            Date = transaction.Date,
            TransactionType = transaction.TransactionType,
            CategoryId = transaction.CategoryId,
            CategoryName = categoryName
        };
    }
}
