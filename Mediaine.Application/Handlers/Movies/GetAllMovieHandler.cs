using MediatR;
using Mediaine.Application.DTOs.Movie;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Handlers.Movies;

public class GetAllMoviesHandler 
    : IRequestHandler<GetAllMoviesRequest, IReadOnlyList<MovieDto>>
{
    private readonly IMovieService _movieService;

    public GetAllMoviesHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public Task<IReadOnlyList<MovieDto>> Handle(
        GetAllMoviesRequest request,
        CancellationToken cancellationToken)
    {
        return _movieService.GetAllAsync();
    }
}