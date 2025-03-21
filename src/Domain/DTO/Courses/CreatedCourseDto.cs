using Domain.DTO.Auth;

namespace Domain.DTO.Courses;
public class CreatedCourseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public bool Availability { get; set; }
    public CreatedUserDto? Instructor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }

    public static explicit operator CreatedCourseDto(Domain.Course.Course course) => new CreatedCourseDto
    {
        Id = course.Id,
        Title = course.Title,
        Description = course.Description,
        Duration = course.Duration,
        Availability = course.Availability,
        Instructor = course.Instructor != null ? (CreatedUserDto)course.Instructor : null,
        CreatedAt = course.CreatedAt,
        UpdatedAt = course.UpdatedAt,
        CreatedById = course.CreatedById,
        ModifiedBy = course.ModifiedBy,
        IsSoftDeleted = course.IsSoftDeleted
    };
}
