using FluentValidation;
using Mediaine.Application.Requests.Categories;

namespace Mediaine.Application.Validators.Category;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .Must(x => x != "string")
            .MaximumLength(100);
    }
}