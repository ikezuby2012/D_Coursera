using Domain.Assignments;
using Domain.DTO.Courses;

namespace Domain.DTO.Exam;
public sealed class ExamQuestionsDto
{
    public Guid Id { get; set; }
    public Guid InstructorId { get; set; }
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
    public List<QuestionList> questionLists { get; set; }

    public static explicit operator ExamQuestionsDto(Domain.Exams.Exams exam) => new ExamQuestionsDto
    {
        Id = exam.Id,
        InstructorId = exam.InstructorId,
        Title = exam.Title,
        Course = exam.Course != null ? (GetAllCoursesDto)exam.Course : null,
        Description = exam.Description,
        TotalMarks = exam.TotalMarks,
        PassingMarks = exam.PassingMarks,
        Status = exam.Status,
        Duration = exam.Duration,
        CreatedAt = exam.CreatedAt,
        CreatedById = exam.CreatedById,
        EndTime = exam.EndTime,
        StartTime = exam.StartTime,
        ModifiedBy = exam.ModifiedBy,
        UpdatedAt = exam.UpdatedAt,
        Instructions = exam.Instructions,
    };
}

public sealed class OptionsTags
{
    public string OptionText { get; set; }
    public string OptionLabel { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedById { get; set; }

    public static explicit operator OptionsTags(Domain.Exams.ExamQuestionOption options) => new OptionsTags
    {
        OptionLabel = options.OptionLabel,
        OptionText = options.OptionText,
        CreatedAt = options.CreatedAt,
        CreatedById = options.CreatedById,
    };
}

public sealed class QuestionList
{
    public Guid id { get; set; }
    public string QuestionText { get; set; }
    public string ExamType { get; set; }
    public List<OptionsTags> Options { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedById { get; set; }

    public static explicit operator QuestionList(Domain.Exams.ExamQuestions examQuestion) => new QuestionList
    {
        id = examQuestion.Id,
        QuestionText = examQuestion.QuestionText,
        ExamType = Exams.ExamType.FromValue(examQuestion.TypeId)?.Name!,
        CreatedAt = examQuestion.CreatedAt,
        CreatedById = examQuestion.CreatedById,
        Options = examQuestion.Options.Select(x => (OptionsTags)x).ToList()
    };
}
