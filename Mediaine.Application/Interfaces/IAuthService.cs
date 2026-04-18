using Mediaine.Application.DTOs.Auth;
using Mediaine.Application.DTOs.Profile;

namespace Mediaine.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(string email, string password);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    Task<bool> LogoutAsync(string refreshToken);
    Task<CurrentUserDto?> GetCurrentUserProfileAsync();
}