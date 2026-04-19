using Mediaine.Application.DTOs.Auth;
using Mediaine.Application.DTOs.Profile;

namespace Mediaine.Application.Abstractions.Services;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(string email, string password);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    Task<bool> LogoutAsync(string refreshToken);
    Task<CurrentUserDto?> GetCurrentUserProfileAsync();
    Task<AuthResponseDto> RegisterAsync(string name, string email, string password);
}