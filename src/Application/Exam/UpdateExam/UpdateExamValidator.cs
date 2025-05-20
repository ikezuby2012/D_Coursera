using FluentValidation;

namespace Application.Exam.UpdateExam;
public class UpdateExamValidator : AbstractValidator<UpdateExamCommand>
{
    private readonly List<string> _allowedStatuses = new()
    {
        "Scheduled",
        "InProgress",
        "Completed",
        "PostPoned",
        "Cancelled"
    };

    public UpdateExamValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Exam ID is required.");

        When(x => x.StartTime.HasValue, () => RuleFor(x => x.EndTime)
                .NotEmpty()
                .WithMessage("End time must be provided when start time is provided.")
                .GreaterThan(x => x.StartTime)
                .WithMessage("End time must be greater than start time."));

        When(x => !string.IsNullOrWhiteSpace(x.Status), () => RuleFor(x => x.Status)
                .Must(status => _allowedStatuses.Contains(status!))
                .WithMessage($"Status must be one of: {string.Join(", ", _allowedStatuses)}"));

        RuleFor(x => x.Title)
            .MaximumLength(200)
            .When(x => !string.IsNullOrWhiteSpace(x.Title))
            .WithMessage("Title must be less than 500 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("Description must be less than 2000 characters.");

        RuleFor(x => x.Instructions)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrWhiteSpace(x.Instructions))
            .WithMessage("Instructions must be less than 2000 characters.");

        When(x => x.TotalMarks.HasValue, () => RuleFor(x => x.TotalMarks)
                .GreaterThan(0)
                .WithMessage("Total marks must be greater than 0."));

        When(x => x.PassingMarks.HasValue, () => RuleFor(x => x.PassingMarks)
                .GreaterThan(0)
                .WithMessage("Passing marks must be greater than 0.")
                .LessThanOrEqualTo(x => x.TotalMarks)
                .When(x => x.TotalMarks.HasValue)
                .WithMessage("Passing marks cannot be greater than total marks."));
    }
}
