using MediatR;
using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs.Category;

namespace Mediaine.Application.Requests.Categories;

public class GetAllCategoriesRequest : PagedRequest, IRequest<PaginationResponse<CategoryDto>>
{
}