using FluentValidation;

namespace Application.Courses.UpdateCourseById;
public class UpdateCourseByIdCommandValidator : AbstractValidator<UpdateCourseByIdCommand>
{
    public UpdateCourseByIdCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Course Id is required");
        RuleFor(x => x.title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration is required");
        RuleFor(x => x.Availability).NotNull();
    }
}
