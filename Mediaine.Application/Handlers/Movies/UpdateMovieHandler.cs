using MediatR;
using Mediaine.Application.DTOs.Movie;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Handlers.Movies;

public class UpdateMovieHandler : IRequestHandler<UpdateMovieRequest, MovieDto?>
{
    private readonly IMovieService _movieService;

    public UpdateMovieHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public Task<MovieDto?> Handle(
        UpdateMovieRequest request,
        CancellationToken cancellationToken)
    {
        return _movieService.UpdateAsync(
            request.Id,
            request.Title,
            request.Price,
            request.CategoryId,
            request.UserId
        );
    }
}