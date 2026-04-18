using MediatR;
using Mediaine.Application.DTOs.Movie;

namespace Mediaine.Application.Requests.Movies;

public class UpdateMovieRequest : IRequest<MovieDto?>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
}