using Domain.DTO.Auth;
using Domain.DTO.Courses;

namespace Domain.DTO.Enrollment;
public sealed class EnrollmentSuccessResponseDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
    public string StatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }

    public static explicit operator EnrollmentSuccessResponseDto(Domain.Enrollment.Enrollment enroll) => new EnrollmentSuccessResponseDto
    {
        Id = enroll.Id,
        CourseId = enroll.CourseId,
        UserId = enroll.UserId,
        StatusId = Domain.Enrollment.EnrollmentStatus.FromValue(enroll.StatusId)!.Name,
        CreatedAt = enroll.CreatedAt,
        UpdatedAt = enroll.UpdatedAt,
        CreatedById = enroll.CreatedById,
        ModifiedBy = enroll.ModifiedBy,
    };
}
