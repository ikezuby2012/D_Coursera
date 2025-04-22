using Application.Abstractions.Messaging;
using Domain.DTO.Assignment;

namespace Application.Assignments.GetAssignmentById;

public sealed record GetAssignmentByIdQuery(Guid Id) : ICommand<CreatedAssignmentDto>;
