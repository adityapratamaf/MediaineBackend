using Mediaine.Application.Abstractions.Storage;
using Mediaine.Application.Abstractions.Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Mediaine.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private static readonly string[] AllowedImageTypes =
    [
        "image/jpeg",
        "image/jpg"
    ];

    private readonly IWebHostEnvironment _environment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocalFileStorageService(
        IWebHostEnvironment environment,
        IHttpContextAccessor httpContextAccessor)
    {
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<FileUploadResult> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        string folder,
        CancellationToken cancellationToken = default)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new Exception("File tidak boleh kosong");

        if (!AllowedImageTypes.Contains(contentType.ToLower()))
            throw new Exception("File harus berupa JPG atau JPEG");

        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        if (extension is not ".jpg" and not ".jpeg")
            throw new Exception("Extension file harus .jpg atau .jpeg");

        var uploadsRoot = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads", folder);
        Directory.CreateDirectory(uploadsRoot);

        var newFileName = $"{Guid.NewGuid():N}{extension}";
        var fullPath = Path.Combine(uploadsRoot, newFileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await fileStream.CopyToAsync(stream, cancellationToken);

        var relativePath = Path.Combine("uploads", folder, newFileName).Replace("\\", "/");
        var request = _httpContextAccessor.HttpContext?.Request;

        var fullUrl = request is null
            ? $"/{relativePath}"
            : $"{request.Scheme}://{request.Host}/{relativePath}";

        return new FileUploadResult
        {
            FileName = newFileName,
            RelativePath = relativePath,
            FullUrl = fullUrl,
            Size = fileStream.Length,
            ContentType = contentType
        };
    }

    public Task DeleteFileAsync(
        string? relativePath,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return Task.CompletedTask;

        var cleanPath = relativePath.Replace("/", Path.DirectorySeparatorChar.ToString());
        var fullPath = Path.Combine(_environment.WebRootPath ?? "wwwroot", cleanPath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }
}