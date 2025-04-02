using FluentValidation;

namespace Application.Media.DeleteMediaById;
internal class DeleteMediaByIdCommandValidator : AbstractValidator<DeleteMediaByIdCommand>
{
    public DeleteMediaByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid MediaId.");
    }
}
