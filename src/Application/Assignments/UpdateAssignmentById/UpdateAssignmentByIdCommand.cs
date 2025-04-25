using Application.Abstractions.Messaging;
using Domain.DTO.Assignment;

namespace Application.Assignments.UpdateAssignmentById;

public sealed record UpdateAssignmentByIdCommand(Guid id, string? Title, string? Description, string? CollectionName, int? AssignmentType, double? MaxScore, DateTime? DueDate) : ICommand<AssigmentResponseDto>;
