using Mediaine.Application.DTOs.Auth;
using Mediaine.Application.DTOs.Profile;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Abstractions.Security;
using Mediaine.Application.Abstractions.Common;
using Mediaine.Application.Abstractions.Persistence;
using Mediaine.Domain.Entities;

namespace Mediaine.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        ICurrentUserService currentUserService,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _currentUserService = currentUserService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDto> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
            throw new Exception("Email atau password salah");

        var validPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash);
        if (!validPassword)
            throw new Exception("Email atau password salah");

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var refreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _userRepository.AddRefreshTokenAsync(refreshToken);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(5),
            RefreshTokenExpiresAt = refreshToken.ExpiresAt,
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await _userRepository.GetRefreshTokenAsync(refreshToken);
        if (storedToken is null || !storedToken.IsActive || storedToken.User is null)
            throw new Exception("Refresh token tidak valid");

        storedToken.IsRevoked = true;
        storedToken.RevokedAt = DateTime.UtcNow;
        await _userRepository.UpdateRefreshTokenAsync(storedToken);

        var user = storedToken.User;

        var newAccessToken = _jwtTokenGenerator.GenerateAccessToken(user);
        var newRefreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();

        var newRefreshToken = new RefreshToken
        {
            Token = newRefreshTokenValue,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _userRepository.AddRefreshTokenAsync(newRefreshToken);

        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(5),
            RefreshTokenExpiresAt = newRefreshToken.ExpiresAt,
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<bool> LogoutAsync(string refreshToken)
    {
        var storedToken = await _userRepository.GetRefreshTokenAsync(refreshToken);
        if (storedToken is null) return false;

        storedToken.IsRevoked = true;
        storedToken.RevokedAt = DateTime.UtcNow;
        await _userRepository.UpdateRefreshTokenAsync(storedToken);

        return true;
    }

    public async Task<CurrentUserDto?> GetCurrentUserProfileAsync()
    {
        if (!_currentUserService.IsAuthenticated || !_currentUserService.UserId.HasValue)
            return null;

        var user = await _userRepository.GetByIdAsync(_currentUserService.UserId.Value);
        if (user is null) return null;

        return new CurrentUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}