using FluentValidation;
using Mediaine.Application.Requests.Categories;

namespace Mediaine.Application.Validators.Category;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Must(x => x != "string")
            .WithMessage("Nama category wajib diisi")
            .MaximumLength(100);
    }
}