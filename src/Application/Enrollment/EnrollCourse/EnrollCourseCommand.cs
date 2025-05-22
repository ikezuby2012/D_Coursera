using Application.Abstractions.Messaging;
using Domain.DTO.Enrollment;

namespace Application.Enrollment.EnrollCourse;
public sealed record EnrollCourseCommand(Guid CourseId) : ICommand<EnrollmentSuccessResponseDto>;
