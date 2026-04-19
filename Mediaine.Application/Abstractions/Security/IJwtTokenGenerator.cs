using Mediaine.Domain.Entities;

namespace Mediaine.Application.Abstractions.Security;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}