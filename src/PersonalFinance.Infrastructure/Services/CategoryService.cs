using PersonalFinance.Application.DTOs.Categories;
using PersonalFinance.Application.Interfaces.Services;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Infrastructure.Repositories.Interfaces;

namespace PersonalFinance.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto request, CancellationToken cancellationToken = default)
    {
        var normalizedName = ValidateAndNormalizeName(request.Name);

        if (await _categoryRepository.NameExistsAsync(normalizedName, null, cancellationToken))
        {
            throw new InvalidOperationException($"Category '{normalizedName}' already exists.");
        }

        var category = new Category
        {
            Name = normalizedName
        };

        var created = await _categoryRepository.AddAsync(category, cancellationToken);
        return MapToResponse(created);
    }

    public async Task<IReadOnlyCollection<CategoryResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        return categories.Select(MapToResponse).ToList();
    }

    public async Task<CategoryResponseDto> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        if (category is null)
        {
            throw new KeyNotFoundException($"Category with id {categoryId} was not found.");
        }

        return MapToResponse(category);
    }

    public async Task<CategoryResponseDto> UpdateAsync(int categoryId, CategoryUpdateDto request, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        if (category is null)
        {
            throw new KeyNotFoundException($"Category with id {categoryId} was not found.");
        }

        var normalizedName = ValidateAndNormalizeName(request.Name);
        if (await _categoryRepository.NameExistsAsync(normalizedName, categoryId, cancellationToken))
        {
            throw new InvalidOperationException($"Category '{normalizedName}' already exists.");
        }

        category.Name = normalizedName;
        var updated = await _categoryRepository.UpdateAsync(category, cancellationToken);
        return MapToResponse(updated);
    }

    public async Task DeleteAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        if (!await _categoryRepository.ExistsByIdAsync(categoryId, cancellationToken))
        {
            throw new KeyNotFoundException($"Category with id {categoryId} was not found.");
        }

        if (await _categoryRepository.HasTransactionsAsync(categoryId, cancellationToken))
        {
            throw new InvalidOperationException("Category cannot be deleted because it is used by existing transactions.");
        }

        await _categoryRepository.DeleteAsync(categoryId, cancellationToken);
    }

    private static CategoryResponseDto MapToResponse(Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    private static string ValidateAndNormalizeName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name is required.");
        }

        return name.Trim();
    }
}
