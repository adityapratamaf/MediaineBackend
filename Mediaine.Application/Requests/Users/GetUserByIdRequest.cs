using MediatR;
using Mediaine.Application.DTOs;

namespace Mediaine.Application.Requests.Users;

public class GetUserByIdRequest : IRequest<UserDto?>
{
    public int Id { get; set; }
}