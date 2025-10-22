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
    public string Category { get; set; }
    public string CourseLevel { get; set; }
    public string Language { get; set; }
    public string? TimeZone { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<TimeLineMedia>? TimeLineMedias { get; set; } = new();

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
        IsSoftDeleted = course.IsSoftDeleted,
        Category = course.Category,
        CourseLevel = course.CourseLevel,
        Language = course.Language,
        TimeZone = course.TimeZone,
        StartDate = course.StartDate,
        EndDate = course.EndDate,
        TimeLineMedias = course.TimelineMedias?.Select(x => new TimeLineMedia
        {
            MediaUrl = x.MediaUrl,
            CreatedAt = x.CreatedAt
        }).ToList(),
    };
}

public class TimeLineMedia
{
    public string MediaUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
