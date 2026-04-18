using MediatR;

namespace Mediaine.Application.Requests.Movies;

public class DeleteMovieRequest : IRequest<bool>
{
    public int Id { get; set; }
}