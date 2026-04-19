using Mediaine.Domain.Entities;

namespace Mediaine.Application.Abstractions.Persistence;

public interface ICategoryRepository
{
    Task<(IReadOnlyList<Category> Items, int TotalData)> GetPagedAsync(int page, int pageSize, string? search);
    Task<Category?> GetByIdAsync(int id);
    Task<Category> AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);
}