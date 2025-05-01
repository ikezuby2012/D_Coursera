
using Domain.AssignmentSubmission;
using Domain.DTO.Assignment;

namespace Domain.DTO.AssignmentSubmission;

public sealed class AssignmentSubmissionResponseDto
{
    public Guid Id { get; set; }
    public AssigmentResponseDto Assignment { get; set; }
    public string SubmissionText { get; set; }
    public string? FileUrl { get; set; }
    public string? Feedback { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? GradedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }

    public static explicit operator AssignmentSubmissionResponseDto(AssignmentSubmissions assignmentSubmissions) => new AssignmentSubmissionResponseDto
    {
        Id = assignmentSubmissions.Id,
        Assignment = (AssigmentResponseDto)assignmentSubmissions.Assignment,
        SubmissionText = assignmentSubmissions.SubmissionText,
        FileUrl = assignmentSubmissions.FileUrl,
        Feedback = assignmentSubmissions.Feedback,
        CreatedAt = assignmentSubmissions.CreatedAt,
        UpdatedAt = assignmentSubmissions.UpdatedAt,
        GradedAt = assignmentSubmissions.GradedAt,
        CreatedById = assignmentSubmissions.CreatedById,
        ModifiedBy = assignmentSubmissions.ModifiedBy,
    };
}
