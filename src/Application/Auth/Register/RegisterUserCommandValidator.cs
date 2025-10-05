using FluentValidation;

namespace Application.Auth.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private static readonly int[] AllowedRoleIds = new[] { 1, 3 };
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.RoleId).Must(roleId => AllowedRoleIds.Contains(roleId)).WithMessage("Unanthorized Role Id");
    }
}
