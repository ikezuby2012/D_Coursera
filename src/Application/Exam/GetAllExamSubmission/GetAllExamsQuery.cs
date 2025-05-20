using Application.Abstractions.Messaging;
using Application.Exam.GetAllCourseExam;

namespace Application.Exam.GetAllExamSubmission;
public sealed record GetAllExamsQuery(int PageSize = 1000, int pageNumber = 0, DateTime? DateFrom = null, DateTime? DateTo = null) : IQuery<GetAllCourseExamResponse>;

