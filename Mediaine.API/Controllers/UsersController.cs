using MediatR;
using Mediaine.Application.Requests.Users;
using Microsoft.AspNetCore.Mvc;

namespace Mediaine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdRequest { Id = id });

        if (result is null)
            return NotFound(new { message = "User tidak ditemukan" });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
    {
        request.Id = id;

        var result = await _mediator.Send(request);

        if (result is null)
            return NotFound(new { message = "User tidak ditemukan" });

        return Ok(result);
    }
}