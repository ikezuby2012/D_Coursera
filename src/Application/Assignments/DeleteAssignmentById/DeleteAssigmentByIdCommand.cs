using Application.Abstractions.Messaging;

namespace Application.Assignments.DeleteAssignmentById;
public sealed record DeleteAssigmentByIdCommand(Guid Id) : ICommand;
