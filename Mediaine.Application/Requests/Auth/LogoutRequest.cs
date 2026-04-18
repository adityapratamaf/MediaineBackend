using MediatR;

namespace Mediaine.Application.Requests.Auth;

public class LogoutRequest : IRequest<bool>
{
    public string RefreshToken { get; set; } = string.Empty;
}