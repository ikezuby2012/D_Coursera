using SharedKernel;

namespace Domain.Course;
public static class CourseErrors
{
    public static Error NotFound(Guid courseId) => Error.NotFound(
        "Course.NotFound",
        $"The Course with the Id = '{courseId}' was not found");
    public static Error NoCoursesFound => Error.NotFound(
        "Course.NoCoursesFound",
        $"No courses found or repository returned");
    public static Error FailedToFetchCourses(string message) => Error.Failure(
        "Course.FailedToFetchCourses",
        $"No courses found or repository returned, error message is {message}");
}
