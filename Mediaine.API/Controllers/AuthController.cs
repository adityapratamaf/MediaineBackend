using MediatR;
using Mediaine.API.Extensions;
using Mediaine.Application.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Mediaine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _mediator.Send(request);

        Response.SetAccessTokenCookie(result.AccessToken);
        Response.SetRefreshTokenCookie(result.RefreshToken);

        return Ok(new
        {
            message = "Login berhasil",
            user = new
            {
                result.UserId,
                result.Name,
                result.Email,
                result.Role
            }
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        if (string.IsNullOrWhiteSpace(refreshToken))
            return Unauthorized(new { message = "Refresh token tidak ditemukan" });

        var result = await _mediator.Send(new RefreshTokenRequest
        {
            RefreshToken = refreshToken
        });

        Response.SetAccessTokenCookie(result.AccessToken);
        Response.SetRefreshTokenCookie(result.RefreshToken);

        return Ok(new { message = "Token berhasil diperbarui" });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refresh_token"];

        if (!string.IsNullOrWhiteSpace(refreshToken))
        {
            await _mediator.Send(new LogoutRequest
            {
                RefreshToken = refreshToken
            });
        }

        Response.ClearAuthCookies();
        return Ok(new { message = "Logout berhasil" });
    }
}