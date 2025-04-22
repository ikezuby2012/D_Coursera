using Domain.DTO.Courses;
using Domain.Media;

namespace Domain.DTO.Assignment;
public sealed class CreatedAssignmentDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid CourseId { get; set; }
    public GetAllCoursesDto? Course { get; set; }
    public string? AssignmentType { get; set; }
    public DateTime DueDate { get; set; }
    public double MaxScore { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CreatedAt { get; set; }

    public static explicit operator CreatedAssignmentDto(Assignments.Assignment assignments) => new CreatedAssignmentDto
    {
        Id = assignments.Id,
        Title = assignments.Title,
        Description = assignments.Description,
        CourseId = assignments.CourseId,
        Course = assignments.Course != null
            ? (GetAllCoursesDto)assignments.Course
            : null,
        AssignmentType = assignments.AssignmentTypeId != null ? Domain.AssignmentType.AssignmentTypes.FromValue((int)assignments.AssignmentTypeId)?.Name : null,
        DueDate = assignments.DueDate,
        MaxScore = assignments.MaxScore,
        CreatedAt = assignments.CreatedAt,
        UpdatedAt = assignments.UpdatedAt,
    };
}
