using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs.Category;

namespace Mediaine.Application.Abstractions.Services;

public interface ICategoryService
{
    Task<PaginationResponse<CategoryDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(string name);
    Task<CategoryDto?> UpdateAsync(int id, string name);
    Task<bool> DeleteAsync(int id);
}