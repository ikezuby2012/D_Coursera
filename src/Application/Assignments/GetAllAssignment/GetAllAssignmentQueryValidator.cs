using FluentValidation;

namespace Application.Assignments.GetAllAssignment;
public class GetAllAssignmentQueryValidator : AbstractValidator<GetAllAssignmentQuery>
{
    public GetAllAssignmentQueryValidator()
    {
        // Validate PageSize
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
            .LessThanOrEqualTo(1000).WithMessage("PageSize cannot exceed 1000.");

        // Validate PageNumber
        RuleFor(x => x.pageNumber)
            .GreaterThanOrEqualTo(0).WithMessage("PageNumber must be greater than or equal to 0.");

        // Validate DateFrom and DateTo
        RuleFor(x => x.DateFrom)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DateFrom cannot be in the future.")
            .When(x => x.DateFrom.HasValue);

        RuleFor(x => x.DateTo)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DateTo cannot be in the future.")
            .When(x => x.DateTo.HasValue);

        RuleFor(x => x)
            .Must(x => !x.DateFrom.HasValue || !x.DateTo.HasValue || x.DateFrom <= x.DateTo)
            .WithMessage("DateFrom must be less than or equal to DateTo.");

        // Validate CourseId
        RuleFor(x => x.CourseId)
            .Must(BeAValidGuid).WithMessage("CourseId must be a valid GUID.")
            .When(x => x.CourseId.HasValue);

        // Validate CollectionName
        RuleFor(x => x.CollectionName)
            .MaximumLength(255).WithMessage("CollectionName must not exceed 255 characters.")
            .When(x => !string.IsNullOrEmpty(x.CollectionName));
    }
    private bool BeAValidGuid(Guid? guid)
    {
        return guid.HasValue && Guid.TryParse(guid.ToString(), out _);
    }
}
