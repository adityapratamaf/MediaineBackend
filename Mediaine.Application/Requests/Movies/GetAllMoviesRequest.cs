using MediatR;
using Mediaine.Application.DTOs.Movie;

namespace Mediaine.Application.Requests.Movies;

public class GetAllMoviesRequest : IRequest<IReadOnlyList<MovieDto>>
{
}