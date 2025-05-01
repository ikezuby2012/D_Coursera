using Application.Abstractions.Messaging;

namespace Application.AssignmentSubmission.DeleteById;
public sealed record DeleteAssignmentSubmissionByIdCommand(Guid Id) : ICommand<Guid>;

