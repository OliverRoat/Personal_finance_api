using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<bool> NameExistsAsync(string name, int? excludingCategoryId = null, CancellationToken cancellationToken = default);
    Task<bool> HasTransactionsAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<string?> GetNameByIdAsync(int categoryId, CancellationToken cancellationToken = default);
}
