using Application.Abstractions.Messaging;
using Domain.DTO.Exam;

namespace Application.Exam.UpdateExam;

public sealed record UpdateExamCommand(
    Guid Id,
    string? Title,
    string? Description,
    decimal? TotalMarks,
    decimal? PassingMarks,
    DateTime? StartTime,
    DateTime? EndTime,
    string? Instructions,
    string? Status
    ) : ICommand<ExamResponseDto>;
