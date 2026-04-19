using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs.Category;
using Mediaine.Application.Requests.Categories;

public class GetAllCategoriesHandler 
    : IRequestHandler<GetAllCategoriesRequest, PaginationResponse<CategoryDto>>
{
    private readonly ICategoryService _categoryService;

    public GetAllCategoriesHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Task<PaginationResponse<CategoryDto>> Handle(
        GetAllCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        return _categoryService.GetAllAsync(request.Page, request.PageSize, request.Search);
    }
}