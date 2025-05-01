using FluentValidation;

namespace Application.Courses.DeleteCourseById;
public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
    }
}
