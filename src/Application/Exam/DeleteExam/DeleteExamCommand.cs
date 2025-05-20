using Application.Abstractions.Messaging;

namespace Application.Exam.DeleteExam;
public sealed record DeleteExamCommand(Guid Id) : ICommand;
