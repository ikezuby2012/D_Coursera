using Application.Abstractions.Messaging;

namespace Application.Assignments.GetAllAssignment;

public sealed record GetAllAssignmentQuery(int PageSize = 1000,
    int pageNumber = 0,
    DateTime? DateFrom = null,
    DateTime? DateTo = null,
    Guid? CourseId = null,
    string? CollectionName = null) : ICommand<GetAllAssignmentResponse>;
