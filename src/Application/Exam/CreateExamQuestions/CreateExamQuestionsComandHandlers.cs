using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Exam;
using Domain.Exams;
using SharedKernel;

namespace Application.Exam.CreateExamQuestions;
internal sealed class CreateExamQuestionsComandHandler(IUnitOfWork unitOfWork, IUserContext userContext, IApplicationDbContext context) : ICommandHandler<CreateExamQuestionCommand, CreatedExamResponseDto>
{
    public async Task<Result<CreatedExamResponseDto>> Handle(CreateExamQuestionCommand request, CancellationToken cancellationToken)
    {
        Exams? exam = await unitOfWork.ExamRepository.GetAsync(x => x.Id == request.ExamId, includeProperties: "Course", cancellationToken: cancellationToken);

        if (exam is null)
        {
            return Result.Failure<CreatedExamResponseDto>(ExamError.NotFound(request.ExamId));
        }

        if (request.QuestonList == null || !request.QuestonList.Any())
        {
            return Result.Failure<CreatedExamResponseDto>(ExamError.NoQuestionsProvided);
        }

        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.DatabaseFacade.BeginTransactionAsync(cancellationToken);

        try
        {
            var examQuestions = new List<ExamQuestions>();
            var optionsToAdd = new List<ExamQuestionOption>();

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
