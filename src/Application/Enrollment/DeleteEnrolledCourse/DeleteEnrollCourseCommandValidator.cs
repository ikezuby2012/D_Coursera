using FluentValidation;

namespace Application.Enrollment.DeleteEnrolledCourse;
public class DeleteEnrollCourseCommandValidator : AbstractValidator<DeleteEnrollCourseCommand>
{
    public DeleteEnrollCourseCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID is required.");
    }
}
