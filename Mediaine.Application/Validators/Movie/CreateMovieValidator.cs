using FluentValidation;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Validators.Movie;

public class CreateMovieValidator : AbstractValidator<CreateMovieRequest>
{
    private static readonly string[] AllowedImageTypes =
    [
        "image/jpeg",
        "image/jpg"
    ];

    public CreateMovieValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title wajib diisi")
            .Must(x => x != "string")
            .WithMessage("Title tidak boleh default 'string'")
            .MaximumLength(150);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price harus lebih dari 0");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Category wajib dipilih");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User wajib diisi");

        RuleFor(x => x.ImageFileName)
            .Must(fileName =>
                string.IsNullOrWhiteSpace(fileName) ||
                Path.GetExtension(fileName).ToLowerInvariant() is ".jpg" or ".jpeg")
            .WithMessage("Image harus berekstensi .jpg atau .jpeg");

        RuleFor(x => x.ImageContentType)
            .Must(contentType =>
                string.IsNullOrWhiteSpace(contentType) ||
                AllowedImageTypes.Contains(contentType.ToLower()))
            .WithMessage("Image harus berupa JPG atau JPEG");
    }
}