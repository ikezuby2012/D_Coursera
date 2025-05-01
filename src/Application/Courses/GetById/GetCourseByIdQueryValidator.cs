using FluentValidation;

namespace Application.Courses.GetById;
public class GetCourseByIdQueryValidator : AbstractValidator<GetCourseByIdQuery>
{
    public GetCourseByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
