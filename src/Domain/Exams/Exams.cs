using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Users;
using SharedKernel;

namespace Domain.Exams;

[Table("TBL_EXAM")]
public sealed class Exams : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TotalMarks { get; set; }
    public decimal PassingMarks { get; set; }
    public Guid CourseId { get; set; }
    public Domain.Course.Course Course { get; set; }
    public Guid InstructorId { get; set; }
    public User Instructor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Instructions { get; set; }

    [AllowedValues("Scheduled, InProgress, Completed, PostPoned, Cancelled")]
    public string Status { get; set; }
    [NotMapped]
    public TimeSpan Duration => EndTime - StartTime;
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
