using Application.Abstractions.Messaging;
using Domain.DTO.Exam;

namespace Application.Exam.CreateExamQuestions;
public sealed record CreateExamQuestionCommand(Guid ExamId, List<QuestionListDto>? QuestonList, int ExamTypeId) : ICommand<CreatedExamResponseDto>;
