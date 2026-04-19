using Mediaine.Application.DTOs.Category;

namespace Mediaine.Application.Abstractions.Services;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(string name);
    Task<CategoryDto?> UpdateAsync(int id, string name);
    Task<bool> DeleteAsync(int id);
}