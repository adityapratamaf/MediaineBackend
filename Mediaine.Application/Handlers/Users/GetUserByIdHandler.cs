using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.DTOs;
using Mediaine.Application.Requests.Users;

namespace Mediaine.Application.Handlers.Users;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserDto?>
{
    private readonly IUserService _userService;

    public GetUserByIdHandler(IUserService userService)
    {
        _userService = userService;
    }

    public Task<UserDto?> Handle(
        GetUserByIdRequest request,
        CancellationToken cancellationToken)
    {
        return _userService.GetByIdAsync(request.Id);
    }
}