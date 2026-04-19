using FluentValidation;
using Mediaine.Application.Requests.Movies;

namespace Mediaine.Application.Validators.Movies;

public class UpdateMovieValidator : AbstractValidator<UpdateMovieRequest>
{
    public UpdateMovieValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0);

        RuleFor(x => x.UserId)
            .GreaterThan(0);
    }
}