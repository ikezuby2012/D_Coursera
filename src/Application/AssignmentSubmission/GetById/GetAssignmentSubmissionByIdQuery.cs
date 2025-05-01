using Application.Abstractions.Messaging;
using Domain.DTO.AssignmentSubmission;

namespace Application.AssignmentSubmission.GetById;

public sealed record GetAssignmentSubmissionByIdQuery(Guid Id) : IQuery<AssignmentSubmissionResponseDto>;
