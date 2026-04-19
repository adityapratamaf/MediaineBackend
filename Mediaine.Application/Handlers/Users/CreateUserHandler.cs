using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.DTOs;
using Mediaine.Application.Requests.Users;

namespace Mediaine.Application.Handlers.Users;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, UserDto>
{
    private readonly IUserService _userService;

    public CreateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public Task<UserDto> Handle(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        return _userService.CreateAsync(
            request.Name,
            request.Email,
            request.Password,
            request.Role
        );
    }
}