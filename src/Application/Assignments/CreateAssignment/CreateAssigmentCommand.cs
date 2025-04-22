using Application.Abstractions.Messaging;
using Domain.DTO.Assignment;

namespace Application.Assignments.CreateAssignment;
public sealed record CreateAssigmentCommand(string title, string description, string CollectionName, string CourseId, DateTime DueDate, double MaxScore, int Type) : ICommand<CreatedAssignmentDto>;
