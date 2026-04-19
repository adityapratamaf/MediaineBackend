using Mediaine.Application.Abstractions.Persistence;
using Mediaine.Domain.Entities;
using Mediaine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mediaine.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MediaineDbContext _context;

    public CategoryRepository(MediaineDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category> AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<(IReadOnlyList<Category> Items, int TotalData)> GetPagedAsync(
        int page,
        int pageSize,
        string? search)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim().ToLower();

            query = query.Where(x =>
                x.Name.ToLower().Contains(keyword));
        }

        var totalData = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalData);
    }
}