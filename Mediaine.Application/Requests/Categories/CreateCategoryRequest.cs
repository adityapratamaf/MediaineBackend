using MediatR;
using Mediaine.Application.DTOs.Category;

namespace Mediaine.Application.Requests.Categories;

public class CreateCategoryRequest : IRequest<CategoryDto>
{
    public string Name { get; set; } = string.Empty;
}