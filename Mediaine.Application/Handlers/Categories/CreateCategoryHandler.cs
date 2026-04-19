using MediatR;
using Mediaine.Application.DTOs.Category;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Requests.Categories;

namespace Mediaine.Application.Handlers.Categories;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, CategoryDto>
{
    private readonly ICategoryService _categoryService;

    public CreateCategoryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Task<CategoryDto> Handle(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        return _categoryService.CreateAsync(request.Name);
    }
}