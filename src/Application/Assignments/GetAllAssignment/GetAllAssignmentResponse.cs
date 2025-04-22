namespace Application.Assignments.GetAllAssignment;
internal class GetAllAssignmentResponse
{
    public IEnumerable<Domain.DTO.Assignment.AssigmentResponseDto> data { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
