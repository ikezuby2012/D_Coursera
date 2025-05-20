using Domain.DTO.Exam;
using FluentValidation;

namespace Application.Exam.CreateExam;
public class CreateExamValidator : AbstractValidator<CreateExamCommand>
{
    public CreateExamValidator()
    {
        /// Add validation stuffs 
        RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required.")
           .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.TotalMarks)
            .GreaterThan(0).WithMessage("Total marks must be greater than 0.");

        RuleFor(x => x.PassingMarks)
            .GreaterThanOrEqualTo(0).WithMessage("Passing marks cannot be negative.")
            .LessThanOrEqualTo(x => x.TotalMarks).WithMessage("Passing marks must be less than or equal to total marks.");

        RuleFor(x => x.ExamTypeId)
            .GreaterThan(0).WithMessage("ExamTypeId must be a valid ID.");

        RuleFor(x => x.startDate)
            .LessThan(x => x.endDate).WithMessage("Start date must be earlier than end date.");

        RuleForEach(x => x.QuestonList)
            .SetValidator(new QuestionListDtoValidator())
            .When(x => x.QuestonList != null && x.QuestonList.Any());
    }
}

public sealed class QuestionListDtoValidator : AbstractValidator<QuestionListDto>
{
    public QuestionListDtoValidator()
    {
        RuleFor(q => q.Question)
            .NotEmpty().WithMessage("Question text is required.");

        RuleFor(q => q.Options)
            .NotNull().WithMessage("Options list must be provided.")
            .Must(o => o.Count >= 2).WithMessage("Each question must have at least 2 options.");

        RuleFor(q => q.Options)
            .Must(HaveOneCorrectOption).WithMessage("Each question must have at least one correct option.");

        RuleForEach(q => q.Options)
            .Must(optionDict => optionDict.Keys.All(k => !string.IsNullOrWhiteSpace(k)))
            .WithMessage("Option keys must not be empty.");

        RuleForEach(q => q.Options)
            .Must(optionDict => optionDict.Values.All(v => !string.IsNullOrWhiteSpace(v.Name)))
            .WithMessage("Each option must have a name.");
    }

    private bool HaveOneCorrectOption(List<Dictionary<string, OptionLabel>> options)
    {
        return options.SelectMany(dict => dict.Values).Any(opt => opt.IsCorrect);
    }
}
