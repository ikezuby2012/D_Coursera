using Application.Abstractions.Messaging;

namespace Application.AssignmentSubmission.GetAll;

public sealed record GetAllAssignmentSubmissionQuery(
    int PageSize = 1000,
    int pageNumber = 0,
    DateTime? DateFrom = null,
    DateTime? DateTo = null,
    Guid? assignmentId = null) : IQuery<GetAllAssignmentSubmissionResponse>;
