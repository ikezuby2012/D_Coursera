using Application.Abstractions.Messaging;
using Domain.DTO.AssignmentSubmission;
using Microsoft.AspNetCore.Http;

namespace Application.AssignmentSubmission.Create;
public sealed record CreateAssignmentSubmissionCommand(Guid assignmentId, string submissionText, IFormFile? file, string? feedback) : ICommand<CreatedAssignmentSubmissionDto>;
