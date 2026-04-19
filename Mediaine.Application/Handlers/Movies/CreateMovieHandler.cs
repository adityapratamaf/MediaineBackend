using MediatR;
using Mediaine.Application.Abstractions.Services;
using Mediaine.Application.Abstractions.Storage;
using Mediaine.Application.DTOs.Movie;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Handlers.Movies;

public class CreateMovieHandler : IRequestHandler<CreateMovieRequest, MovieDto>
{
    private readonly IMovieService _movieService;
    private readonly IFileStorageService _fileStorageService;

    public CreateMovieHandler(
        IMovieService movieService,
        IFileStorageService fileStorageService)
    {
        _movieService = movieService;
        _fileStorageService = fileStorageService;
    }

    public async Task<MovieDto> Handle(
        CreateMovieRequest request,
        CancellationToken cancellationToken)
    {
        string? filePath = null;

        if (request.ImageStream is not null &&
            !string.IsNullOrWhiteSpace(request.ImageFileName) &&
            !string.IsNullOrWhiteSpace(request.ImageContentType))
        {
            var uploadResult = await _fileStorageService.UploadAsync(
                request.ImageStream,
                request.ImageFileName,
                request.ImageContentType,
                "movies",
                cancellationToken);

            filePath = uploadResult.RelativePath;
        }

        return await _movieService.CreateAsync(
            request.Title,
            request.Price,
            request.CategoryId,
            request.UserId,
            filePath
        );
    }
}