using Application.Abstractions.Messaging;
using Domain.DTO.Exam;

namespace Application.Exam.GetExam;
public sealed record GetExamByIdQuery(Guid Id) : IQuery<ExamResponseDto>;
