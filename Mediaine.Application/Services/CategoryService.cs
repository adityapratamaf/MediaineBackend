using AutoMapper;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Abstractions.Persistence;
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

    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
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