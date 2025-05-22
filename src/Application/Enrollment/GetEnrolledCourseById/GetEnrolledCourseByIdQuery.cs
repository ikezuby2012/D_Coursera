using Application.Abstractions.Messaging;
using Domain.DTO.Enrollment;

namespace Application.Enrollment.GetEnrolledCourseById;
public sealed record GetEnrolledCourseByIdQuery(Guid Id) : IQuery<EnrollmentSuccessResponseDto>;
