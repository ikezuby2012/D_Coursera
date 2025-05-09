using Application.Abstractions.Messaging;
using Domain.DTO.Exam;

namespace Application.Exam.CreateExam;
public sealed record CreateExamCommand(string Title, Guid CourseId, string Description, decimal TotalMarks, decimal PassingMarks, string? Instructions, List<QuestionListDto>? QuestonList, int ExamTypeId, DateTime startDate, DateTime endDate) : ICommand<CreatedExamResponseDto>;
