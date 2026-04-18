using Mediaine.Application.DTOs;

namespace Mediaine.Application.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(string fullName, string email, string password, string role);
    Task<UserDto?> UpdateAsync(int id, string fullName, string email, string role);
}