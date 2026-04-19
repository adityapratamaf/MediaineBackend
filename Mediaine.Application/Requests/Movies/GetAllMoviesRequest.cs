using MediatR;
using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs.Movie;

namespace Mediaine.Application.Requests.Movies;

public class GetAllMoviesRequest : PagedRequest, IRequest<PaginationResponse<MovieDto>>
{
}