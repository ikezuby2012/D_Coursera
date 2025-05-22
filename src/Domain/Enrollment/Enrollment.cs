using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Users;
using SharedKernel;

namespace Domain.Enrollment;

[Table("TBL_ENROLLMENT")]
public sealed class Enrollment : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course.Course Course { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int StatusId { get; set; }
    public EnrollmentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
