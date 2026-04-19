using MediatR;
using Mediaine.Application.Common.Models;
using Mediaine.Application.DTOs;

namespace Mediaine.Application.Requests.Users;

public class GetAllUsersRequest : PagedRequest, IRequest<PaginationResponse<UserDto>>
{
}