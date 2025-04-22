using Domain.Common;

namespace Domain.AssignmentType;
public sealed class AssignmentTypes : Enumeration<AssignmentTypes>
{
    public static readonly AssignmentTypes Quiz = new(1, "QUIZ");
    public static readonly AssignmentTypes Essay = new(2, "ESSAY");
    public static readonly AssignmentTypes Upload = new(3, "UPLOAD");
    public static readonly AssignmentTypes MultipleChoice = new(4, "MULTIPLE_CHOICE");
    private AssignmentTypes(int Id, string name) : base(Id, name) { }
}
