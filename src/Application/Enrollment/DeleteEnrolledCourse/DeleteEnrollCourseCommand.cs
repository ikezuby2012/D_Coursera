using Application.Abstractions.Messaging;

namespace Application.Enrollment.DeleteEnrolledCourse;
public sealed record DeleteEnrollCourseCommand(Guid Id) : ICommand<Guid>;
