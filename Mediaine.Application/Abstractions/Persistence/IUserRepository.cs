using Mediaine.Domain.Entities;

namespace Mediaine.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<(IReadOnlyList<User> Items, int TotalData)> GetPagedAsync(int page, int pageSize, string? search);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
}