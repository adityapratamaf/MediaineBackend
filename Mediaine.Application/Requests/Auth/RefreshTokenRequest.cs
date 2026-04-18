using MediatR;
using Mediaine.Application.DTOs.Auth;

namespace Mediaine.Application.Requests.Auth;

public class RefreshTokenRequest : IRequest<AuthResponseDto>
{
    public string RefreshToken { get; set; } = string.Empty;
}