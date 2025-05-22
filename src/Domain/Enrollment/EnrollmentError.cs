using SharedKernel;

namespace Domain.Enrollment;
public static class EnrollmentError
{
    public static Error UserAlreadyEnrolled => Error.NotFound(
    "Enrollment.UserAlreadyEnrolled",
    "User is already enrolled in this course");

    public static Error MaximumCapacity => Error.NotFound(
    "Enrollment.MaximumCapacity",
    "Course has reached maximum capacity");

    public static Error FailedToEnrollUser(string exception) => Error.NotFound(
        "Enrollment.FailedToEnrollUser",

        @$"Failed to enroll user to this course 
           
           ----------------------------------------------

            {exception}");

    public static Error NotFound(Guid Id) => Error.NotFound(
        "Enrollment.NotFound",
        $"no details was found with this {Id}");

}
