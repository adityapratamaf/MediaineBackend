using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs;

namespace Mediaine.Application.Abstractions.Services;

public interface IUserService
{
    Task<PaginationResponse<UserDto>> GetAllAsync(int page, int pageSize, string? search);
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(string name, string email, string password, string role);
    Task<UserDto?> UpdateAsync(int id, string name, string email, string role);
}