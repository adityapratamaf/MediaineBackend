using MediatR;
using Mediaine.Application.DTOs;

namespace Mediaine.Application.Requests.Users;

public class CreateUserRequest : IRequest<UserDto>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}