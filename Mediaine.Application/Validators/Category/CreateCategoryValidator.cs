using FluentValidation;
using Mediaine.Application.Requests.Categories;

namespace Mediaine.Application.Validators.Category;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nama category wajib diisi")
            .MaximumLength(100);
    }
}