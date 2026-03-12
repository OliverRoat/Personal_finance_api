using Microsoft.EntityFrameworkCore;
using PersonalFinance.Infrastructure.Data;
using PersonalFinance.Infrastructure.Repositories.Interfaces;

namespace PersonalFinance.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsByIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
    }

    public Task<string?> GetNameByIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Categories
            .Where(c => c.Id == categoryId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
