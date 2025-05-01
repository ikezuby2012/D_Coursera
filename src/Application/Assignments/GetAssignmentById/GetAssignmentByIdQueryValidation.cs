using FluentValidation;

namespace Application.Assignments.GetAssignmentById;
public class GetAssignmentByIdQueryValidation : AbstractValidator<GetAssignmentByIdQuery>
{
    public GetAssignmentByIdQueryValidation()
    {
        RuleFor(x => x.Id.ToString()).NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");
    }

    private bool BeAValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}
