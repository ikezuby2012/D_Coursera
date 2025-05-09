using Domain.Common;

namespace Domain.Exams;
public sealed class ExamType : Enumeration<ExamType>
{
    public static readonly ExamType MultipleChoice = new(1, "Multiple Choice");
    public static readonly ExamType TrueFalse = new(2, "True or False");
    public static readonly ExamType FillInTheBlank = new(3, "Fill In The Blank");
    public static readonly ExamType MatchTheFollowing = new(4, "Match The Following");
    public static readonly ExamType BriefAnswer = new(5, "Brief Answer Only");
    private ExamType(int Id, string name) : base(Id, name) { }
}
