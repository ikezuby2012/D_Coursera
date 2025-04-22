using Assignment = Domain.Assignments.Assignment;

namespace Application.Abstractions.Interface;
public interface IAssignmentRepository : IRepository<Assignment>
{
    void Update(Assignment assignment);
}
