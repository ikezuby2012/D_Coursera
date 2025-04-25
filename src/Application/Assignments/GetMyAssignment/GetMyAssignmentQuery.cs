using Application.Abstractions.Messaging;
using Application.Assignments.GetAllAssignment;

namespace Application.Assignments.GetMyAssignment;

public sealed record GetMyAssignmentQuery(int PageSize = 1000,
    int pageNumber = 0,
    DateTime? DateFrom = null,
    DateTime? DateTo = null,
    Guid? CourseId = null,
    string? CollectionName = null) : ICommand<GetAllAssignmentResponse>;
