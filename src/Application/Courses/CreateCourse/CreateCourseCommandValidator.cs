using FluentValidation;

namespace Application.Courses.CreateCourse;
internal sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration is required");
        RuleFor(x => x.Availability).NotNull();
    }
}
