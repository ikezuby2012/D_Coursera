using FluentValidation;

namespace Application.Media.CreateMedia;
public class CreateMediaCommandValidator : AbstractValidator<CreateMediaCommand>
{
    public CreateMediaCommandValidator()
    {
        RuleFor(x => x.files)
           .NotEmpty().WithMessage("At least one file must be uploaded.")
           .Must(files => files.All(f => f.Length > 0)).WithMessage("Uploaded files must not be empty.");

        RuleFor(x => x.courseId)
            .NotEmpty().WithMessage("CourseId is required.")
            .NotEqual(Guid.Empty).WithMessage("Invalid CourseId.");
    }
}
