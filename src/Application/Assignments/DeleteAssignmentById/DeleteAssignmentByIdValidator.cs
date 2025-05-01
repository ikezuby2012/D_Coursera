using FluentValidation;

namespace Application.Assignments.DeleteAssignmentById;
public class DeleteAssignmentByIdValidator : AbstractValidator<DeleteAssigmentByIdCommand>
{
    public DeleteAssignmentByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid Id.");
    }
}
