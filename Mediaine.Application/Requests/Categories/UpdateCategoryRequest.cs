using MediatR;
using Mediaine.Application.DTOs.Category;

namespace Mediaine.Application.Requests.Categories;

public class UpdateCategoryRequest : IRequest<CategoryDto?>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}