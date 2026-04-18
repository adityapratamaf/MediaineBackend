using MediatR;
using Mediaine.Application.DTOs.Profile;
using Mediaine.Application.Interfaces;
using Mediaine.Application.Requests.Profile;

namespace Mediaine.Application.Handlers;

public class ProfileHandler : IRequestHandler<GetCurrentUserProfileRequest, CurrentUserDto?>
{
    private readonly IAuthService _authService;

    public ProfileHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<CurrentUserDto?> Handle(GetCurrentUserProfileRequest request, CancellationToken cancellationToken)
        => _authService.GetCurrentUserProfileAsync();
}