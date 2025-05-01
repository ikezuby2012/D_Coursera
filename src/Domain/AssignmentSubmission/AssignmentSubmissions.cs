using System.ComponentModel.DataAnnotations.Schema;
using Domain.Assignments;
using Domain.Common;
using Domain.Users;
using SharedKernel;

namespace Domain.AssignmentSubmission;

[Table("TBL_ASSIGNMENT_SUBMISSION")]
public sealed class AssignmentSubmissions : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public Guid SubmittedById { get; set; }
    public User SubmittedBy { get; set; }
    public string SubmissionText { get; set; }
    public string FileUrl { get; set; }
    public string? Feedback { get; set; }
    public double? Grade { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? GradedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
