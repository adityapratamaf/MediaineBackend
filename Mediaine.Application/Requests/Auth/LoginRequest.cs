using MediatR;
using Mediaine.Application.DTOs.Auth;

namespace Mediaine.Application.Requests.Auth;

public class LoginRequest : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}