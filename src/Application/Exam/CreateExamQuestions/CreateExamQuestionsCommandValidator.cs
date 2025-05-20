using Application.Abstractions.Data;
using Application.Exam.CreateExam;
using FluentValidation;

namespace Application.Exam.CreateExamQuestions;

public class CreateExamQuestionsCommandValidator : AbstractValidator<CreateExamQuestionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateExamQuestionsCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.ExamId).NotEmpty().WithMessage("ExamId is required.")
               .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.")
               .MustAsync(ExamExists).WithMessage("The specified Exam Id does not exist.");

        RuleForEach(x => x.QuestonList)
           .SetValidator(new QuestionListDtoValidator())
           .When(x => x.QuestonList != null && x.QuestonList.Any());

        _unitOfWork = unitOfWork;
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
