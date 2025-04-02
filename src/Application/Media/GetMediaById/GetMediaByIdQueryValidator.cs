using FluentValidation;

namespace Application.Media.GetMediaById;
internal sealed class GetMediaByIdQueryValidator : AbstractValidator<GetMediaByIdQuery>
{
    public GetMediaByIdQueryValidator()
    {
        RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Id is required.")
         .NotEqual(Guid.Empty).WithMessage("Invalid MediaId.");
    }
}
