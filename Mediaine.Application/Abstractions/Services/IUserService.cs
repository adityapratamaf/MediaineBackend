using Mediaine.Application.DTOs;

namespace Mediaine.Application.Abstractions.Services;

public interface IUserService
{
    Task<IReadOnlyList<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(string fullName, string email, string password, string role);
    Task<UserDto?> UpdateAsync(int id, string fullName, string email, string role);
}