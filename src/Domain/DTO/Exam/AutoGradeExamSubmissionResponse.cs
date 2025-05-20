using Domain.DTO.Auth;

namespace Domain.DTO.Exam;
public sealed class AutoGradeExamSubmissionResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public CreatedUserDto? Student { get; set; }
    public Guid ExamId { get; set; }
    public double? CorrectAnswers { get; set; }
    public double? CorrectScore { get; set; }
    public double ExamTotalScore { get; set; }
    public double TotalQuestions { get; set; }
    public double? MarkedQuestions { get; set; }
    public bool IsGraded { get; set; }
    public string? CreatedById { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; }

    public static AutoGradeExamSubmissionResponse ToJsonResponse(Domain.Exams.Exams exam, Domain.Exams.ExamsSubmission submission, Domain.Users.User student) => new AutoGradeExamSubmissionResponse
    {
        Id = submission.Id,
        Description = exam.Description,
        Student = (CreatedUserDto)student,
        ExamId = exam.Id,
        IsGraded = true,
        CreatedById = submission.CreatedById,
        StartTime = submission.StartTime,
        EndTime = submission.EndTime,
        CreatedAt = submission.CreatedAt,
    };
}
