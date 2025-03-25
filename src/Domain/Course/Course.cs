using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Users;
using SharedKernel;

namespace Domain.Course;

[Table("TBL_COURSES")]
public sealed class Course : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public bool Availability { get; set; }
    public Guid InstructorId { get; set; }
    public User Instructor { get; set; }
    public ICollection<Domain.Media.Media> MediaUrls { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
