using PersonalFinance.Application.DTOs.Categories;

namespace PersonalFinance.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<CategoryResponseDto> CreateAsync(CategoryCreateDto request, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<CategoryResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CategoryResponseDto> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default);
    Task<CategoryResponseDto> UpdateAsync(int categoryId, CategoryUpdateDto request, CancellationToken cancellationToken = default);
    Task DeleteAsync(int categoryId, CancellationToken cancellationToken = default);
}
