using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs.Movie;

namespace Mediaine.Application.Abstractions.Services;

public interface IMovieService
{
    Task<PaginationResponse<MovieDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<MovieDto?> GetByIdAsync(int id);
    Task<MovieDto> CreateAsync(string title, decimal price, int categoryId, int userId);
    Task<MovieDto?> UpdateAsync(int id, string title, decimal price, int categoryId, int userId);
    Task<bool> DeleteAsync(int id);
}