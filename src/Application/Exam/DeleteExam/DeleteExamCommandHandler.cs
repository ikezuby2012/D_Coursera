using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Exams;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Exam.DeleteExam;
internal sealed class DeleteExamCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, IApplicationDbContext context) : ICommandHandler<DeleteExamCommand>
{
    public async Task<Result> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
    {
        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.DatabaseFacade.BeginTransactionAsync(cancellationToken);

        try
        {
            Domain.Exams.Exams? exam = await unitOfWork.ExamRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (exam is null)
            {
                return Result.Failure(ExamError.NotFound(request.Id));
            }

            if (exam.InstructorId == userContext.UserId)
            {
                return Result.Failure(Domain.Exams.ExamError.UnAuthorizedAccess);
            }

            exam.IsSoftDeleted = true;
            exam.ModifiedBy = userContext.UserId.ToString();
            exam.UpdatedAt = DateTime.UtcNow;
            exam.Status = "Cancelled";

            unitOfWork.ExamRepository.Update(exam);

            List<Domain.Exams.ExamQuestions> examQuestions = await unitOfWork.ExamQuestionsRepository.QueryAble().Where(x => x.ExamId == request.Id).ToListAsync(cancellationToken);

            foreach (ExamQuestions examQuestion in examQuestions)
            {
                examQuestion.IsSoftDeleted = true;
                examQuestion.UpdatedAt = DateTime.UtcNow;
                examQuestion.ModifiedBy = userContext.UserId.ToString();
            }

            unitOfWork.ExamQuestionsRepository.UpdateRangeAsync(examQuestions, cancellationToken);

            var questionIds = examQuestions.Select(q => q.Id).ToList();

            List<ExamQuestionOption> examOptions = await unitOfWork.ExamQuestionOptionsRepository.QueryAble().Where(o => questionIds.Contains(o.QuestionId)).ToListAsync(cancellationToken);

            foreach (ExamQuestionOption? option in examOptions)
            {
                option.IsSoftDeleted = true;
                option.UpdatedAt = DateTime.UtcNow;
                option.ModifiedBy = userContext.UserId.ToString();
            }

            unitOfWork.ExamQuestionOptionsRepository.UpdateRangeAsync(examOptions, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(ExamError.FailedToUpdateExamCredentials(ex.Message));
        }

        return Result.Success();
    }
}
