using Application.Abstractions.Interface;
using Domain.Enrollment;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;
public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
{
    private readonly ApplicationDbContext _db;

    public EnrollmentRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Enrollment enrollment)
    {
        _db.Enrollment.Update(enrollment);
    }
}
