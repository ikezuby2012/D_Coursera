using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using SharedKernel;

namespace Domain.Exams;

[Table("TBL_EXAM_QUESTIONS")]
public sealed class ExamQuestions : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Exams Exam { get; set; }
    public string QuestionText { get; set; }
    public int TypeId { get; set; }
    public ExamType Type { get; set; }
    public ICollection<ExamQuestionOption> Options { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
