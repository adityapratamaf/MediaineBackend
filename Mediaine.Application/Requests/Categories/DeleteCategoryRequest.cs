using MediatR;

namespace Mediaine.Application.Requests.Categories;

public class DeleteCategoryRequest : IRequest<bool>
{
    public int Id { get; set; }
}