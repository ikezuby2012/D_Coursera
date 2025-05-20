using Application.Abstractions.Messaging;

namespace Application.Exam.GetAllCourseExam;
public sealed record GetAllCourseExamQuery(
    int PageSize = 1000,
    int pageNumber = 0,
    DateTime? DateFrom = null,
    DateTime? DateTo = null,
    Guid? CourseId = null) : IQuery<GetAllCourseExamResponse>;
