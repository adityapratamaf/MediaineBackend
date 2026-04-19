using MediatR;
using Mediaine.Application.DTOs.Auth;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Requests.Auth;

namespace Mediaine.Application.Handlers;

public class AuthHandler :
    IRequestHandler<LoginRequest, AuthResponseDto>,
    IRequestHandler<RefreshTokenRequest, AuthResponseDto>,
    IRequestHandler<LogoutRequest, bool>
{
    private readonly IAuthService _authService;

    public AuthHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<AuthResponseDto> Handle(LoginRequest request, CancellationToken cancellationToken)
        => _authService.LoginAsync(request.Email, request.Password);

    public Task<AuthResponseDto> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        => _authService.RefreshTokenAsync(request.RefreshToken);

    public Task<bool> Handle(LogoutRequest request, CancellationToken cancellationToken)
        => _authService.LogoutAsync(request.RefreshToken);
}