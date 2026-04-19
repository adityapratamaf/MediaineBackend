using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.DTOs.Auth;
using Mediaine.Application.Requests.Auth;

namespace Mediaine.Application.Handlers.Auth;

public class RegisterHandler : IRequestHandler<RegisterRequest, AuthResponseDto>
{
    private readonly IAuthService _authService;

    public RegisterHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<AuthResponseDto> Handle(
        RegisterRequest request,
        CancellationToken cancellationToken)
    {
        return _authService.RegisterAsync(
            request.Name,
            request.Email,
            request.Password
        );
    }
}