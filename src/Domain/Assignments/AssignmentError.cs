using SharedKernel;

namespace Domain.Assignments;
public static class AssignmentError
{
    public static Error InvalidCourseId(string arg) => Error.NotFound(
      "Assignment.invalidCourseId",
      $"not a valid Course id -- {arg}!");

    public static Error NotFound(Guid assignmentId) => Error.NotFound(
        "Assigment.NotFound",
        $"The Assignment with ID '{assignmentId}' not found");
}
