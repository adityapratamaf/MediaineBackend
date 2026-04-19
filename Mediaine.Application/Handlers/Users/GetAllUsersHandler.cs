using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs;
using Mediaine.Application.Requests.Users;

namespace Mediaine.Application.Handlers.Users;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, PaginationResponse<UserDto>>
{
    private readonly IUserService _userService;

    public GetAllUsersHandler(IUserService userService)
    {
        _userService = userService;
    }

    public Task<PaginationResponse<UserDto>> Handle(
        GetAllUsersRequest request,
        CancellationToken cancellationToken)
    {
        return _userService.GetAllAsync(request.Page, request.PageSize, request.Search);
    }
}