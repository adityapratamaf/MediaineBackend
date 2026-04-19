using MediatR;
using Mediaine.Application.DTOs.Category;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Requests.Categories;

namespace Mediaine.Application.Handlers.Categories;

public class GetAllCategoriesHandler 
    : IRequestHandler<GetAllCategoriesRequest, IReadOnlyList<CategoryDto>>
{
    private readonly ICategoryService _categoryService;

    public GetAllCategoriesHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Task<IReadOnlyList<CategoryDto>> Handle(
        GetAllCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        return _categoryService.GetAllAsync();
    }
}