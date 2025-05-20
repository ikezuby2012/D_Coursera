using Application.Abstractions.Data;
using Domain.DTO.Exam;
using FluentValidation;

namespace Application.Exam.AutoGradeExamSubmission;
public class AutoGradeExamSubmissionCommandValidator : AbstractValidator<AutoGradeExamSubmissionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public AutoGradeExamSubmissionCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.ExamId).NotEmpty().WithMessage("ExamId is required.")
                .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.")
                .MustAsync(ExamExists).WithMessage("The specified Exam Id does not exist.");

        RuleFor(x => x.StartTime)
           .LessThan(x => x.EndTime).WithMessage("Start time must be before End time.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime).WithMessage("End time must be after Start time.");

        RuleFor(x => x.AnswersScripts)
            .NotEmpty().WithMessage("Answer scripts are required.");

        RuleForEach(x => x.AnswersScripts).SetValidator(new ExamSubmissionAnswerValidator());
    }

    private bool BeAValidGuid(Guid examId)
    {
        return examId != Guid.Empty;
    }

    private async Task<bool> ExamExists(Guid examId, CancellationToken cancellationToken)
    {
        // Query the database to check if the course exists
        Domain.Exams.Exams? exam = await _unitOfWork.ExamRepository.GetAsync(
            x => x.Id == examId,
            cancellationToken: cancellationToken
        );

        return exam != null;
    }
}

public class ExamSubmissionAnswerValidator : AbstractValidator<ExamSubmissionRequestDto>
{
    public ExamSubmissionAnswerValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty().WithMessage("QuestionId is required.")
            .Must(id => id != Guid.Empty).WithMessage("QuestionId must be a valid GUID.");

        RuleFor(x => x.OptionLabel)
            .NotEmpty().WithMessage("OptionLabel is required.");

        RuleFor(x => x.OptionText)
            .NotEmpty().WithMessage("OptionText is required.");
    }
}
