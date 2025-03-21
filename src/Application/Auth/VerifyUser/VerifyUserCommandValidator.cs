using FluentValidation;

namespace Application.Auth.VerifyUser;
internal sealed class VerifyUserCommandValidator : AbstractValidator<VerifyUserCommand>
{
    public VerifyUserCommandValidator()
    {
        RuleFor(c => c.email).NotEmpty().EmailAddress();
        RuleFor(c => c.otp).NotEmpty().MinimumLength(6);
    }
}
