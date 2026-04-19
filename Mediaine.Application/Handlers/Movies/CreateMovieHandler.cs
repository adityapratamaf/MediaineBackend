using MediatR;
using Mediaine.Application.DTOs.Movie;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Handlers.Movies;

public class CreateMovieHandler : IRequestHandler<CreateMovieRequest, MovieDto>
{
    private readonly IMovieService _movieService;

    public CreateMovieHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public Task<MovieDto> Handle(
        CreateMovieRequest request,
        CancellationToken cancellationToken)
    {
        return _movieService.CreateAsync(
            request.Title,
            request.Price,
            request.CategoryId,
            request.UserId
        );
    }
}