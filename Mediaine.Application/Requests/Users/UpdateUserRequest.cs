using MediatR;
using Mediaine.Application.DTOs;

namespace Mediaine.Application.Requests.Users;

public class UpdateUserRequest : IRequest<UserDto?>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}