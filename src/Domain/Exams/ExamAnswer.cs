using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using SharedKernel;

namespace Domain.Exams;

[Table("TBL_EXAM_ANSWER")]
public sealed class ExamAnswer : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid SubmissionId { get; set; }
    public ExamsSubmission Submission { get; set; }
    public Guid QuestionId { get; set; }
    public ExamQuestions Questions { get; set; }
    public string AnswerText { get; set; }
    public string OptionLabel { get; set; }
    public bool IsCorrect { get; set; }
    public bool IsGraded { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? GradedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? GradedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
