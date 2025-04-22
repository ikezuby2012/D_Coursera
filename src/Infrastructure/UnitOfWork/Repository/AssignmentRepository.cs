using Application.Abstractions.Interface;
using Infrastructure.Database;

using Assignment = Domain.Assignments.Assignment;

namespace Infrastructure.UnitOfWork.Repository;
public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
{
    private readonly ApplicationDbContext _db;

    public AssignmentRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(Assignment assignment)
    {
        _db.Assignments.Update(assignment);
    }
}
