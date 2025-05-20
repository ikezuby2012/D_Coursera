namespace Application.Exam.GetAllCourseExam;
public sealed class GetAllCourseExamResponse
{
    public IEnumerable<Domain.DTO.Exam.CreatedExamResponseDto> data { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
