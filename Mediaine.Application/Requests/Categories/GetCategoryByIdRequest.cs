using MediatR;
using Mediaine.Application.DTOs.Category;

namespace Mediaine.Application.Requests.Categories;

public class GetCategoryByIdRequest : IRequest<CategoryDto?>
{
    public int Id { get; set; }
}