using Application.Abstractions.Data;
using FluentValidation;

namespace Application.Assignments.CreateAssignment;
internal class CreateAssignmentCommandValidator : AbstractValidator<CreateAssigmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateAssignmentCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.")
                .Must(BeAValidGuid).WithMessage("CourseId must be a valid GUID.")
                .MustAsync(CourseExists).WithMessage("The specified CourseId does not exist.");
        RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("DueDate must be in the future.");
        RuleFor(x => x.MaxScore)
            .GreaterThan(0).WithMessage("MaxScore must be greater than 0.");
        RuleFor(x => x.Type)
            .GreaterThanOrEqualTo(1).WithMessage("Type must be a positive integer.")
            .Must(BeAValidAssignmentType)
            .WithMessage("Invalid assignment type selected.");

    }

    private bool BeAValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }

    private bool BeAValidAssignmentType(int id)
    {
        // Assuming FromValue returns null if not found
        return Domain.AssignmentType.AssignmentTypes.FromValue(id) != null;
    }
    private async Task<bool> CourseExists(string courseId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(courseId, out Guid parsedCourseId))
        {
            return false; // Invalid GUID format
        }

        // Query the database to check if the course exists
        Domain.Course.Course? course = await _unitOfWork.CourseRepository.GetAsync(
            x => x.Id == parsedCourseId,
            cancellationToken: cancellationToken
        );

        return course != null;
    }
}
