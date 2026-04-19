using Mediaine.Application.Abstractions.Common.Models;

namespace Mediaine.Application.Abstractions.Storage;

public interface IFileStorageService
{
    Task<FileUploadResult> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        string folder,
        CancellationToken cancellationToken = default);

    Task DeleteFileAsync(
        string? relativePath,
        CancellationToken cancellationToken = default);
}