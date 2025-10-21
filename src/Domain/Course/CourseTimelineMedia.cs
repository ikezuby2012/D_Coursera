using Domain.Common;
using Domain.Users;
using SharedKernel;

namespace Domain.Course;

public class CourseTimelineMedia : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public string MediaUrl { get; set; }
    public string? FileFormat { get; set; }
    public string? FilePath { get; set; }
    public Guid UploadedById { get; set; }
    public User UploadedBy { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
