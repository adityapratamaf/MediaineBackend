using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs.Movie;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Handlers.Movies;

public class GetAllMoviesHandler : IRequestHandler<GetAllMoviesRequest, PaginationResponse<MovieDto>>
{
    private readonly IMovieService _movieService;

    public GetAllMoviesHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public Task<PaginationResponse<MovieDto>> Handle(
        GetAllMoviesRequest request,
        CancellationToken cancellationToken)
    {
        return _movieService.GetAllAsync(request.Page, request.PageSize, request.Search);
    }
}