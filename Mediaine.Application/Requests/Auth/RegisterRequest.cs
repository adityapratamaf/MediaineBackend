using MediatR;
using Mediaine.Application.DTOs.Auth;

namespace Mediaine.Application.Requests.Auth;

public class RegisterRequest : IRequest<AuthResponseDto>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}