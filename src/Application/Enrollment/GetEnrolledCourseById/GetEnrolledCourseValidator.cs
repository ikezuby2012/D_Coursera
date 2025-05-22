using FluentValidation;

namespace Application.Enrollment.GetEnrolledCourseById;
public class GetEnrolledCourseValidator : AbstractValidator<GetEnrolledCourseByIdQuery>
{
    public GetEnrolledCourseValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Id is required.")
           .NotEqual(Guid.Empty).WithMessage("Invalid Id.");
    }
}
