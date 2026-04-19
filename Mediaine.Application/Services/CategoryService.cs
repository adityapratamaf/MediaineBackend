using AutoMapper;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Abstractions.Persistence;
using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs.Category;
using Mediaine.Domain.Entities;

namespace Mediaine.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    // ✅ PAGINATION + SEARCH
    public async Task<PaginationResponse<CategoryDto>> GetAllAsync(int page, int pageSize, string? search)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var (items, totalData) = await _categoryRepository.GetPagedAsync(page, pageSize, search);

        return new PaginationResponse<CategoryDto>
        {
            Items = _mapper.Map<IReadOnlyList<CategoryDto>>(items),
            CurrentPage = page,
            PageSize = pageSize,
            TotalData = totalData,
            TotalPages = (int)Math.Ceiling((double)totalData / pageSize),
            Search = search
        };
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category is null ? null : _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateAsync(string name)
    {
        var category = new Category
        {
            Name = name
        };

        var created = await _categoryRepository.AddAsync(category);
        return _mapper.Map<CategoryDto>(created);
    }

    public async Task<CategoryDto?> UpdateAsync(int id, string name)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null) return null;

        category.Name = name;
        await _categoryRepository.UpdateAsync(category);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null) return false;

        await _categoryRepository.DeleteAsync(category);
        return true;
    }
}