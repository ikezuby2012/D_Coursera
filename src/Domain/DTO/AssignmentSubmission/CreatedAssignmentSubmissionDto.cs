using Domain.AssignmentSubmission;
using Domain.DTO.Assignment;

namespace Domain.DTO.AssignmentSubmission;

public sealed class CreatedAssignmentSubmissionDto
{
    public Guid Id { get; set; }
    public AssigmentResponseDto Assignment { get; set; }
    public Guid SubmittedBy { get; set; }
    public string? FileUrl { get; set; }
    public string? Feedback { get; set; }
    public DateTime CreatedAt { get; set; }

    public static explicit operator CreatedAssignmentSubmissionDto(AssignmentSubmissions assignmentSubmissions) => new CreatedAssignmentSubmissionDto
    {
        Id = assignmentSubmissions.Id,
        Assignment = (AssigmentResponseDto)assignmentSubmissions.Assignment,
        SubmittedBy = assignmentSubmissions.SubmittedById,
        FileUrl = assignmentSubmissions.FileUrl,
        Feedback = assignmentSubmissions.Feedback,
        CreatedAt = assignmentSubmissions.CreatedAt,
    };
}
