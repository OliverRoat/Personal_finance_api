namespace PersonalFinance.Infrastructure.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<bool> ExistsByIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<string?> GetNameByIdAsync(int categoryId, CancellationToken cancellationToken = default);
}
