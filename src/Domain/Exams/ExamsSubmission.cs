using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using SharedKernel;

namespace Domain.Exams;

[Table("TBL_EXAM_SUBMISSION")]
public sealed class ExamsSubmission : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Users.User Student { get; set; }
    public Guid ExamId { get; set; }
    public Exams Exams { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? GradedAt { get; set; }
    public Guid GradedById { get; set; }
    public Users.User GradedBy { get; set; }
    public double? TotalScore { get; set; }
    public bool IsGraded { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
