using Application.Abstractions.Messaging;
using Domain.DTO.AssignmentSubmission;
using Microsoft.AspNetCore.Http;

namespace Application.AssignmentSubmission.UpdateById;
public sealed record UpdateAssignmentSubmissionByIdCommand(Guid Id, Guid assignmentId, string submissionText, IFormFile? file, string? feedback) : ICommand<UpdatedAssignmentSubmissionDto>;
