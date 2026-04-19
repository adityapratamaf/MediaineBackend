using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.DTOs;
using Mediaine.Application.Requests.Users;

namespace Mediaine.Application.Handlers.Users;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UserDto?>
{
    private readonly IUserService _userService;

    public UpdateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public Task<UserDto?> Handle(
        UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        return _userService.UpdateAsync(
            request.Id,
            request.Name,
            request.Email,
            request.Role
        );
    }
}