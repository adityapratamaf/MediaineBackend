using FluentValidation;
using Mediaine.Application.Requests.Users;

namespace Mediaine.Application.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Must(x => x != "string")
            .WithMessage("Nama wajib diisi")
            .MaximumLength(150);

        RuleFor(x => x.Email)
            .NotEmpty()
            .Must(x => x != "string")
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Must(x => x != "string")
            .MinimumLength(6)
            .WithMessage("Password minimal 6 karakter");

        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(x => x != "string")
            .Must(role => role == "Admin" || role == "User")
            .WithMessage("Role harus Admin atau User");
    }
}