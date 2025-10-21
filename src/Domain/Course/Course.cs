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
    public int? Capacity { get; set; }
    public string? Prerequisites { get; set; }
    public bool IsPaid { get; set; }
    public string Category { get; set; }
    public string CourseLevel { get; set; }
    public string Language { get; set; }
    public string? TimeZone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid InstructorId { get; set; }
    public User Instructor { get; set; }
    public ICollection<Media.Media> MediaUrls { get; set; }
    public ICollection<CourseTimelineMedia> TimelineMedias { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
