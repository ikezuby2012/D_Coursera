using Domain.DTO.Auth;
using Domain.DTO.Courses;
using Domain.Users;

namespace Domain.DTO.Exam;

public sealed class ExamResponseDto
{
    public Guid Id { get; set; }
    public CreatedUserDto? Instructor { get; set; }
    public string? Title { get; set; }
    public string? Instructions { get; set; }
    public GetAllCoursesDto? Course { get; set; }
    public string? Description { get; set; }
    public string? ExamType { get; set; }
    public decimal TotalMarks { get; set; }
    public decimal PassingMarks { get; set; }
    public string Status { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public static explicit operator ExamResponseDto(Domain.Exams.Exams exam) => new ExamResponseDto
    {
        Id = exam.Id,
        Title = exam.Title,
        Course = exam.Course != null ? (GetAllCoursesDto)exam.Course : null,
        Instructor = (CreatedUserDto)exam.Instructor,
        Instructions = exam.Instructions,
        Description = exam.Description,
        TotalMarks = exam.TotalMarks,
        PassingMarks = exam.PassingMarks,
        Status = exam.Status,
        UpdatedAt = exam.UpdatedAt,
        ModifiedBy = exam.ModifiedBy,
        Duration = exam.Duration,
        CreatedAt = exam.CreatedAt,
        CreatedById = exam.CreatedById,
        EndTime = exam.EndTime,
        StartTime = exam.StartTime,
        //ExamType = exam.t != null ? Domain.AssignmentType.AssignmentTypes.FromValue((int)assignments.AssignmentTypeId)?.Name : null,
    };
}
