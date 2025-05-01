using FluentValidation;

namespace Application.Media.GetCourseMedia;
public class GetAllCourseMediaQueryValidation : AbstractValidator<GetAllCourseMediaQuery>
{
    public GetAllCourseMediaQueryValidation()
    {
        RuleFor(x => x.courseId)
           .NotEmpty().WithMessage("CourseId is required.")
           .NotEqual(Guid.Empty).WithMessage("Invalid CourseId.");
    }
}
