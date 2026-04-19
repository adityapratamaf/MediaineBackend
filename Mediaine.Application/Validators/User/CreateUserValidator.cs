using FluentValidation;
using Mediaine.Application.Requests.Users;

namespace Mediaine.Application.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nama wajib diisi")
            .MaximumLength(150);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password minimal 6 karakter");

        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(role => role == "Admin" || role == "User")
            .WithMessage("Role harus Admin atau User");
    }
}