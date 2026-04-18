using MediatR;
using Mediaine.Application.DTOs.Profile;

namespace Mediaine.Application.Requests.Profile;

public class GetCurrentUserProfileRequest : IRequest<CurrentUserDto?>
{
}