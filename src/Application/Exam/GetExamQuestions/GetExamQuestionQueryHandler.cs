using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Exam;
using Domain.Exams;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Exam.GetExamQuestions;
internal sealed class GetExamQuestionQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetExamQuestionQuery, ExamQuestionsDto>
{
    public async Task<Result<ExamQuestionsDto>> Handle(GetExamQuestionQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ExamQuestions>? questions = unitOfWork.ExamQuestionsRepository.QueryAble().Include(q => q.Options).Where(x => x.ExamId == request.Id);

        if (questions is null)
        {
            return Result.Failure<ExamQuestionsDto>(ExamError.ExamQuestionNotFound(request.Id));
        }

        Exams? exam = await unitOfWork.ExamRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (exam is null)
        {
            return Result.Failure<ExamQuestionsDto>(ExamError.NotFound(request.Id));
        }

        List<QuestionList> examQuestions = await questions.Select(x => (QuestionList)x).ToListAsync(cancellationToken);

        var examResponse = (ExamQuestionsDto)exam;
        examResponse.questionLists = examQuestions;

        return Result.Success(examResponse);
    }
}
