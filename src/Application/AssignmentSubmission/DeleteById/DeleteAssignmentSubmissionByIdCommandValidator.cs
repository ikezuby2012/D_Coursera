using FluentValidation;

namespace Application.AssignmentSubmission.DeleteById;

public class DeleteAssignmentSubmissionByIdCommandValidator : AbstractValidator<DeleteAssignmentSubmissionByIdCommand>
{
    public DeleteAssignmentSubmissionByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid Id.")
            .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return Guid.TryParse(guid.ToString(), out _);
    }
}
