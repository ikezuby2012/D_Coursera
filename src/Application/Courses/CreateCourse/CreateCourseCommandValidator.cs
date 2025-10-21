using FluentValidation;

namespace Application.Courses.CreateCourse;
public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration is required");
        RuleFor(x => x.Availability).NotNull();
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.");

        RuleFor(x => x.CourseLevel)
            .NotEmpty().WithMessage("Course level is required.");

        RuleFor(x => x.Language)
            .NotEmpty().WithMessage("Language is required.");

        RuleFor(x => x.TimeZones)
            .NotEmpty().WithMessage("Time zone(s) are required.");

        // Date validation
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date must be on or after the start date.")
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue);
    }
}
