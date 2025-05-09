using SharedKernel;

namespace Domain.Exams;
public static class ExamError
{
    public static Error FailedToUploadExamCredentials(string message) => Error.Failure(
        "Exam.FailedToUploadExamCredentials",
        $"Failed to upload Question, error message is {message}");
}
