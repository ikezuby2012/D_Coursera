using Domain.AssignmentSubmission;
using Domain.DTO.Assignment;

namespace Domain.DTO.AssignmentSubmission;

public sealed class UpdatedAssignmentSubmissionDto
{
    public Guid Id { get; set; }
    public AssigmentResponseDto Assignment { get; set; }
    public Guid SubmittedBy { get; set; }
    public string SubmissionText { get; set; }
    public string? FileUrl { get; set; }
    public string? Feedback { get; set; }
    public DateTime CreatedAt { get; set; }
    public double? Grade { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? GradedAt { get; set; }
    public string? ModifiedBy { get; set; }

    public static explicit operator UpdatedAssignmentSubmissionDto(AssignmentSubmissions assignmentSubmissions) => new UpdatedAssignmentSubmissionDto
    {
        Id = assignmentSubmissions.Id,
        Assignment = (AssigmentResponseDto)assignmentSubmissions.Assignment,
        SubmittedBy = assignmentSubmissions.SubmittedById,
        SubmissionText = assignmentSubmissions.SubmissionText,
        FileUrl = assignmentSubmissions.FileUrl,
        Feedback = assignmentSubmissions.Feedback,
        CreatedAt = assignmentSubmissions.CreatedAt,
        Grade = assignmentSubmissions.Grade,
        UpdatedAt = assignmentSubmissions.UpdatedAt,
        GradedAt = assignmentSubmissions.GradedAt,
        ModifiedBy = assignmentSubmissions.ModifiedBy,
    };
}
