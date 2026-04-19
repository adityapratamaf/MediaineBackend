using Mediaine.Domain.Entities;

namespace Mediaine.Application.Abstractions.Persistence;

public interface IMovieRepository
{
    Task<(IReadOnlyList<Movie> Items, int TotalData)> GetPagedAsync(int page, int pageSize, string? search);
    Task<Movie?> GetByIdAsync(int id);
    Task<Movie> AddAsync(Movie movie);
    Task UpdateAsync(Movie movie);
    Task DeleteAsync(Movie movie);
}