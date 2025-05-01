using AssignmentSubmissions = Domain.AssignmentSubmission.AssignmentSubmissions;

namespace Application.Abstractions.Interface;
public interface IAssignmentSubmissionRepository : IRepository<AssignmentSubmissions>
{
    void Update(AssignmentSubmissions assignmentSubmissions);
}
