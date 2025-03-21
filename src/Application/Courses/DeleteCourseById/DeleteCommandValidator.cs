using FluentValidation;

namespace Application.Courses.DeleteCourseById;
internal sealed class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required!");
    }
}
