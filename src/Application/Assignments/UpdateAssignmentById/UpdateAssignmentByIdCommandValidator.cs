using FluentValidation;

namespace Application.Assignments.UpdateAssignmentById;
internal class UpdateAssignmentByIdCommandValidator : AbstractValidator<UpdateAssignmentByIdCommand>
{
    public UpdateAssignmentByIdCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("Id is required!")
            .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return Guid.TryParse(guid.ToString(), out _);
    }
}
