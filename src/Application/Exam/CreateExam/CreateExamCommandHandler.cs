using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Exam;
using Domain.Exams;
using SharedKernel;

namespace Application.Exam.CreateExam;

internal sealed class CreateExamCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, IApplicationDbContext context) : ICommandHandler<CreateExamCommand, CreatedExamResponseDto>
{
    public async Task<Result<CreatedExamResponseDto>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
    {

        //// transaction to upload to DB
        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.DatabaseFacade.BeginTransactionAsync(cancellationToken);

        Exams? exam;
        try
        {
            exam = new Exams
            {
                Title = request.Title,
                CourseId = request.CourseId,
                Description = request.Description,
                TotalMarks = request.TotalMarks,
                PassingMarks = request.PassingMarks,
                Instructions = request.Instructions,
                CreatedById = userContext.UserId.ToString(),
                InstructorId = userContext.UserId,
                StartTime = request.startDate,
                EndTime = request.endDate,
                CreatedAt = DateTime.UtcNow,
                Status = "InProgress",
                IsSoftDeleted = false,
            };

            await unitOfWork.ExamRepository.AddAsync(exam);
            await unitOfWork.SaveAsync(cancellationToken);

            var examQuestions = new List<ExamQuestions>();
            var optionsToAdd = new List<ExamQuestionOption>();

            if (request.QuestonList != null && request.QuestonList.Any())
            {
                foreach (QuestionListDto questionItem in request.QuestonList)
                {
                    var question = new ExamQuestions
                    {
                        ExamId = exam.Id,
                        QuestionText = questionItem.Question,
                        TypeId = request.ExamTypeId,
                        CreatedAt = DateTime.UtcNow,
                        CreatedById = userContext.UserId.ToString(),
                        IsSoftDeleted = false,
                    };

                    examQuestions.Add(question);
                }

                await unitOfWork.ExamQuestionsRepository.AddRangeAsync(examQuestions, cancellationToken);
                await unitOfWork.SaveAsync(cancellationToken);

                for (int i = 0; i < request.QuestonList.Count; i++)
                {
                    QuestionListDto questionItem = request.QuestonList[i];
                    ExamQuestions quest = examQuestions[i];

                    foreach (Dictionary<string, OptionLabel> optionDict in questionItem.Options)
                    {
                        foreach (KeyValuePair<string, OptionLabel> kvp in optionDict)
                        {
                            var examQuestionOption = new ExamQuestionOption
                            {
                                QuestionId = quest.Id,
                                OptionText = kvp.Value.Name,
                                IsCorrect = kvp.Value.IsCorrect,
                                OptionLabel = kvp.Key,
                                CreatedAt = DateTime.UtcNow,
                                CreatedById = userContext.UserId.ToString(),
                                IsSoftDeleted = false
                            };

                            optionsToAdd.Add(examQuestionOption);
                        }
                    }
                }

                await unitOfWork.ExamQuestionOptionsRepository.AddRangeAsync(optionsToAdd, cancellationToken);
                await unitOfWork.SaveAsync(cancellationToken);

            }
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<CreatedExamResponseDto>(ExamError.FailedToUploadExamCredentials(ex.Message));
        }

        var examResponse = (CreatedExamResponseDto)exam;

        return Result.Success(examResponse);
    }
}
