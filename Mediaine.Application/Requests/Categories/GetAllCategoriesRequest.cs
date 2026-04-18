using MediatR;
using Mediaine.Application.DTOs.Category;

namespace Mediaine.Application.Requests.Categories;

public class GetAllCategoriesRequest : IRequest<List<CategoryDto>>
{
}