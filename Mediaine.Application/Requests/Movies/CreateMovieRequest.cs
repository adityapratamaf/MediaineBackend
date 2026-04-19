using MediatR;
using Mediaine.Application.DTOs.Movie;

namespace Mediaine.Application.Requests.Movies;

public class CreateMovieRequest : IRequest<MovieDto>
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }

    public Stream? ImageStream { get; set; }
    public string? ImageFileName { get; set; }
    public string? ImageContentType { get; set; }
}