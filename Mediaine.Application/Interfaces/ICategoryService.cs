using Mediaine.Application.DTOs.Category;

namespace Mediaine.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(string name);
    Task<CategoryDto?> UpdateAsync(int id, string name);
    Task<bool> DeleteAsync(int id);
}