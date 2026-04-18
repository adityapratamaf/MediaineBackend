using MediatR;
using Mediaine.Application.DTOs.Movie;

namespace Mediaine.Application.Requests.Movies;

public class GetMovieByIdRequest : IRequest<MovieDto?>
{
    public int Id { get; set; }
}