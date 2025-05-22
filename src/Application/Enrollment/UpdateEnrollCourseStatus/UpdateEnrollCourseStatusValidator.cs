using FluentValidation;

namespace Application.Enrollment.UpdateEnrollCourseStatus;
public class UpdateEnrollCourseStatusValidator : AbstractValidator<UpdateEnrollCourseStatusCommand>
{
    private readonly List<string> _allowedStatuses = new()
    {
        "Pending",
        "Approved",
        "Rejected",
        "Completed",
    };
    public UpdateEnrollCourseStatusValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage("ID is required.");

        When(x => !string.IsNullOrWhiteSpace(x.status), () => RuleFor(x => x.status)
                .Must(status => _allowedStatuses.Contains(status!))
                .WithMessage($"not a valid status"));
    }
}
