namespace Domain.DTO.Exam;

public sealed class OptionLabel
{
    public string Name { get; set; }
    public bool IsCorrect { get; set; }
}

public sealed class QuestionListDto
{
    public string Question { get; set; }
    public List<Dictionary<string, OptionLabel>> Options { get; set; }
}
