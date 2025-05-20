using SharedKernel;

namespace Domain.Exams;
public static class ExamError
{
    public static Error FailedToUploadExamCredentials(string message) => Error.Failure(
        "Exam.FailedToUploadExamCredentials",
        $"Failed to upload Question, error message is {message}");

    public static Error NotFound(Guid courseId) => Error.NotFound(
       "Exam.NotFound",
       $"The Exam with the Id = '{courseId}' was not found");

    public static Error UnAuthorizedAccess => Error.NotFound(
        "Exam.UnAuthorizedAccess",
        $"Unauthorized request to delete, request permission from creator of the exam");
    public static Error FailedToUpdateExamCredentials(string message) => Error.Failure(
        "Exam.FailedToUpdateExamCredentials",
        $"Failed to update Question, error message is {message}");

    public static Error ExamQuestionNotFound(Guid Id) => Error.NotFound(
       "Exam.ExamQuestionNotFound",
       $"exam questions  with the id  = '{Id}' was not found");

    public static Error NoAnswersProvided => Error.Problem(
        "Exam.NoAnswersProvided",
        "Answers is missiing in the json payload");

    public static Error NoQuestionsProvided => Error.NotFound(
        "Exam.NoQuestionsProvided",
        $"No Questions was provided!");
}
