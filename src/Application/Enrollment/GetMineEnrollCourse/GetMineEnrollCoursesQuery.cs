using Application.Abstractions.Messaging;
using Application.Enrollment.GetAllEnrollCourse;

namespace Application.Enrollment.GetMineEnrollCourse;
public sealed record GetMineEnrollCoursesQuery(
    int PageSize = 1000,
    int pageNumber = 0,
    DateTime? DateFrom = null,
    DateTime? DateTo = null) : IQuery<GetAllEnrollCoursesResponse>;
