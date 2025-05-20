using FluentValidation;

namespace Application.Exam.GetExam;

public class GetExamByIdQueryValidator : AbstractValidator<GetExamByIdQuery>
{
    public GetExamByIdQueryValidator()
    {
        RuleFor(x => x.Id.ToString()).NotEmpty().WithMessage("Id is required.")
               .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");
    }

    private bool BeAValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}
