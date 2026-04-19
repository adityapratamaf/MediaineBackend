using MediatR;
using Mediaine.Application.Requests.Movies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mediaine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllMoviesRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetMovieByIdRequest { Id = id });

        if (result is null)
            return NotFound(new { message = "Movie tidak ditemukan" });

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create(
        [FromForm] string title,
        [FromForm] decimal price,
        [FromForm] int categoryId,
        [FromForm] int userId,
        IFormFile? image)
    {
        var request = new CreateMovieRequest
        {
            Title = title,
            Price = price,
            CategoryId = categoryId,
            UserId = userId
        };

        if (image is not null)
        {
            request.ImageStream = image.OpenReadStream();
            request.ImageFileName = image.FileName;
            request.ImageContentType = image.ContentType;
        }

        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(
        int id,
        [FromForm] string title,
        [FromForm] decimal price,
        [FromForm] int categoryId,
        [FromForm] int userId,
        IFormFile? image)
    {
        var request = new UpdateMovieRequest
        {
            Id = id,
            Title = title,
            Price = price,
            CategoryId = categoryId,
            UserId = userId
        };

        if (image is not null)
        {
            request.ImageStream = image.OpenReadStream();
            request.ImageFileName = image.FileName;
            request.ImageContentType = image.ContentType;
        }

        var result = await _mediator.Send(request);

        if (result is null)
            return NotFound(new { message = "Movie tidak ditemukan" });

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteMovieRequest { Id = id });

        if (!result)
            return NotFound(new { message = "Movie tidak ditemukan" });

        return NoContent();
    }
}