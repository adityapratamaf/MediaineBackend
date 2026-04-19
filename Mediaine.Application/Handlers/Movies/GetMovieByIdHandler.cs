using MediatR;
using Mediaine.Application.DTOs.Movie;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Handlers.Movies;

public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdRequest, MovieDto?>
{
    private readonly IMovieService _movieService;

    public GetMovieByIdHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public Task<MovieDto?> Handle(
        GetMovieByIdRequest request,
        CancellationToken cancellationToken)
    {
        return _movieService.GetByIdAsync(request.Id);
    }
}