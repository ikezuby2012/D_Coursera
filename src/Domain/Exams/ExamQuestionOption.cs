using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using SharedKernel;

namespace Domain.Exams;

[Table("TBL_EXAM_QUESTION_OPTION")]
public sealed class ExamQuestionOption : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public ExamQuestions Question { get; set; }
    public string OptionText { get; set; }
    public string OptionLabel { get; set; }
    public bool IsCorrect { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
