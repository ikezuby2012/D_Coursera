using Application.Abstractions.Messaging;

namespace Application.Enrollment.GetAllEnrollCourse;
public sealed record GetAllEnrollCourseQuery(
    int PageSize = 1000,
    int pageNumber = 0,
    DateTime? DateFrom = null,
    DateTime? DateTo = null,
    Guid? CourseId = null) : IQuery<GetAllEnrollCoursesResponse>;
