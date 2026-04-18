namespace Mediaine.Application.DTOs.Movie;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
}