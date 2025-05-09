using Domain.Assignments;
using Domain.DTO.Courses;

namespace Domain.DTO.Exam;
public sealed class CreatedExamResponseDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public GetAllCoursesDto? Course { get; set; }
    public string? Description { get; set; }
    public string? ExamType { get; set; }
    public decimal TotalMarks { get; set; }
    public decimal PassingMarks { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedById { get; set; }

    public static explicit operator CreatedExamResponseDto(Domain.Exams.Exams exam) => new CreatedExamResponseDto
    {
        Id = exam.Id,
        Title = exam.Title,
        Course = exam.Course != null ? (GetAllCoursesDto)exam.Course : null,
        Description = exam.Description,
        TotalMarks = exam.TotalMarks,
        PassingMarks = exam.PassingMarks,
        Duration = exam.Duration,
        CreatedAt = exam.CreatedAt,
        CreatedById = exam.CreatedById,
        EndTime = exam.EndTime,
        StartTime = exam.StartTime,
        //ExamType = exam.t != null ? Domain.AssignmentType.AssignmentTypes.FromValue((int)assignments.AssignmentTypeId)?.Name : null,
    };

    public static CreatedExamResponseDto FromExamsAndQuestion(Domain.Exams.Exams exam, Domain.Exams.ExamQuestions examQuestions) => new CreatedExamResponseDto
    {
        Id = exam.Id,
        Title = exam.Title,
        Course = exam.Course != null ? (GetAllCoursesDto)exam.Course : null,
        Description = exam.Description,
        TotalMarks = exam.TotalMarks,
        PassingMarks = exam.PassingMarks,
        Duration = exam.Duration,
        CreatedAt = exam.CreatedAt,
        CreatedById = exam.CreatedById,
        EndTime = exam.EndTime,
        StartTime = exam.StartTime,
        ExamType = Exams.ExamType.FromValue(examQuestions.TypeId)?.Name
    };
}
