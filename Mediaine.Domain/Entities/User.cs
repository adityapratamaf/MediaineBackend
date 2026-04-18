using Mediaine.Domain.Common;

namespace Mediaine.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User";

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}