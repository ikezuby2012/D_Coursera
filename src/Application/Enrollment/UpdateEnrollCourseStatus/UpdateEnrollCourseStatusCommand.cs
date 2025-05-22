using Application.Abstractions.Messaging;
using Domain.DTO.Enrollment;

namespace Application.Enrollment.UpdateEnrollCourseStatus;
public sealed record UpdateEnrollCourseStatusCommand(Guid Id, string status) : ICommand<EnrollmentSuccessResponseDto>;
