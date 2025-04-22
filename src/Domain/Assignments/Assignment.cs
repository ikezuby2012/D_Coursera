using System.ComponentModel.DataAnnotations.Schema;
using Domain.AssignmentType;
using Domain.Common;
using SharedKernel;

namespace Domain.Assignments;

[Table("TBL_ASSIGNMENT")]
public sealed class Assignment : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CourseId { get; set; }
    public Course.Course Course { get; set; }
    public string? CollectionName { get; set; }
    public double MaxScore { get; set; }
    public int? AssignmentTypeId { get; set; }
    public AssignmentTypes? AssignmentTypes { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
