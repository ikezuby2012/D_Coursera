using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Exam;
using Domain.Exams;
using Domain.Users;
using SharedKernel;

namespace Application.Exam.AutoGradeExamSubmission;
internal sealed class AutoGradeExamSubmissionCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, IApplicationDbContext context) : ICommandHandler<AutoGradeExamSubmissionCommand, AutoGradeExamSubmissionResponse>
{
    public async Task<Result<AutoGradeExamSubmissionResponse>> Handle(AutoGradeExamSubmissionCommand request, CancellationToken cancellationToken)
    {
        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.DatabaseFacade.BeginTransactionAsync(cancellationToken);

        try
        {
            Exams? exam = await unitOfWork.ExamRepository.GetAsync(x => x.Id == request.ExamId, tracked: true, cancellationToken: cancellationToken);

            if (exam is null)
            {
                return Result.Failure<AutoGradeExamSubmissionResponse>(ExamError.NotFound(request.ExamId));
            }

            User? student = await unitOfWork.UserRepository.GetAsync(x => x.Id == userContext.UserId, cancellationToken: cancellationToken);

            if (student is null)
            {
                return Result.Failure<AutoGradeExamSubmissionResponse>(UserErrors.NotFound(userContext.UserId));
            }

            var examSubmission = new ExamsSubmission
            {
                StudentId = userContext.UserId,
                ExamId = request.ExamId,
                CreatedAt = DateTime.UtcNow,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CreatedById = userContext.UserId.ToString(),
                GradedById = exam.InstructorId,
                IsSoftDeleted = false
            };

            await unitOfWork.ExamSubmissionRepository.AddAsync(examSubmission);
            await unitOfWork.SaveAsync(cancellationToken);

            var answers = new List<ExamAnswer>();
            int totalQuestions = await unitOfWork.ExamQuestionsRepository.CountAsync(x => x.ExamId == request.ExamId, cancellationToken);
            double totalScore = 0;
            double markedQuestions = 0;

            if (request.AnswersScripts == null || !request.AnswersScripts.Any())
            {
                return Result.Failure<AutoGradeExamSubmissionResponse>(ExamError.NoAnswersProvided);
            }

            foreach (ExamSubmissionRequestDto answer in request.AnswersScripts!)
            {
                ExamQuestionOption? questionOption = await unitOfWork.ExamQuestionOptionsRepository.GetAsync(x => x.QuestionId == answer.QuestionId && x.OptionLabel == answer.OptionLabel, cancellationToken: cancellationToken);

                if (questionOption is null)
                {
                    continue;
                }

                bool isCorrect = questionOption.IsCorrect;

                if (isCorrect)
                {
                    totalScore += 1;
                }

                markedQuestions++;

                var examAnswer = new ExamAnswer
                {
                    SubmissionId = examSubmission.Id,
                    QuestionId = answer.QuestionId,
                    AnswerText = answer.OptionText,
                    OptionLabel = answer.OptionLabel,
                    IsCorrect = isCorrect,
                    IsGraded = true,
                    CreatedAt = DateTime.UtcNow,
                    GradedAt = DateTime.UtcNow
                };

                answers.Add(examAnswer);
            }
            double correctScore = (double)exam.TotalMarks / totalQuestions * totalScore;

            examSubmission.TotalScore = correctScore;

            await unitOfWork.ExamAnswerRepository.AddRangeAsync(answers, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            var response = AutoGradeExamSubmissionResponse.ToJsonResponse(exam, examSubmission, student);
            response.MarkedQuestions = markedQuestions;
            response.CorrectScore = correctScore;
            response.CorrectAnswers = totalScore;
            response.TotalQuestions = totalQuestions;
            response.ExamTotalScore = (double)exam.TotalMarks;

            return Result.Success(response);

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<AutoGradeExamSubmissionResponse>(ExamError.FailedToUploadExamCredentials(ex.Message));
        }
    }
}
