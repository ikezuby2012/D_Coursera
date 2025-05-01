using Application.Abstractions.Interface;
using Domain.AssignmentSubmission;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;
public class AssignmentSubmissionRepository : Repository<AssignmentSubmissions>, IAssignmentSubmissionRepository
{
    private readonly ApplicationDbContext _db;

    public AssignmentSubmissionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(AssignmentSubmissions assignmentSubmissions)
    {
        _db.AssignmentSubmissions.Update(assignmentSubmissions);
    }
}
