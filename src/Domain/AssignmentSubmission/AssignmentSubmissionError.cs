using SharedKernel;

namespace Domain.AssignmentSubmission;
public static class AssignmentSubmissionError
{
    public static Error InvalidAssignmentId(string arg) => Error.NotFound(
    "AssignmentSubmission.invalidAssignmentId", $"not a valid assignment id -- {arg}!");

    public static Error NotFound(Guid Id) => Error.NotFound(
        "AssignmentSubmission.NotFound",
        $"The Assignment Submission with ID '{Id}' not found");
}
