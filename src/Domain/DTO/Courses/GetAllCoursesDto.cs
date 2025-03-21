namespace Domain.DTO.Courses;
public class GetAllCoursesDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public bool Availability { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }

    public static explicit operator GetAllCoursesDto(Domain.Course.Course course) => new GetAllCoursesDto
    {
        Id = course.Id,
        Title = course.Title,
        Description = course.Description,
        Duration = course.Duration,
        Availability = course.Availability,
        CreatedAt = course.CreatedAt,
        UpdatedAt = course.UpdatedAt,
        CreatedById = course.CreatedById,
        ModifiedBy = course.ModifiedBy
    };
}
