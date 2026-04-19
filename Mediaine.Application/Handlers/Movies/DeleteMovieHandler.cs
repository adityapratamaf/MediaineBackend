using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Handlers.Movies;

public class DeleteMovieHandler : IRequestHandler<DeleteMovieRequest, bool>
{
    private readonly IMovieService _movieService;

    public DeleteMovieHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public Task<bool> Handle(
        DeleteMovieRequest request,
        CancellationToken cancellationToken)
    {
        return _movieService.DeleteAsync(request.Id);
    }
}