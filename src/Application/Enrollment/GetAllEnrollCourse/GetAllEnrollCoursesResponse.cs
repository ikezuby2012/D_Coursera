namespace Application.Enrollment.GetAllEnrollCourse;
public sealed class GetAllEnrollCoursesResponse
{
    public IEnumerable<Domain.DTO.Enrollment.EnrollmentSuccessResponseDto> data { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
