using FluentValidation;

namespace Application.Exam.DeleteExam;

public class DeleteExamCommandValidator : AbstractValidator<DeleteExamCommand>
{
    public DeleteExamCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.").NotEqual(Guid.Empty).WithMessage("Invalid Id.");
    }
}
