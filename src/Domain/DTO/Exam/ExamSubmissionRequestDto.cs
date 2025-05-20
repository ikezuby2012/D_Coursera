namespace Domain.DTO.Exam;
public sealed class ExamSubmissionRequestDto
{
    public Guid QuestionId { get; set; }
    public string OptionLabel { get; set; }
    public string OptionText { get; set; }
}
