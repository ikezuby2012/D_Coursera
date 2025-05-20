using Application.Abstractions.Messaging;
using Domain.DTO.Exam;

namespace Application.Exam.GetExamQuestions;
public sealed record GetExamQuestionQuery(Guid Id) : IQuery<ExamQuestionsDto>;
