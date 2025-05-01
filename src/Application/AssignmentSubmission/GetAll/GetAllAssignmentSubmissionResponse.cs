using Domain.DTO.AssignmentSubmission;

namespace Application.AssignmentSubmission.GetAll;
public sealed class GetAllAssignmentSubmissionResponse
{
    public IEnumerable<AssignmentSubmissionResponseDto> data { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
