using FluentValidation;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Validators.Movie;

public class CreateMovieValidator : AbstractValidator<CreateMovieRequest>
{
    public CreateMovieValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title wajib diisi")
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
    }
}
