using Application.Abstractions.Interface;
using Domain.Exams;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;
public class ExamRepository : Repository<Exams>, IExamRepository
{
    private readonly ApplicationDbContext _db;

    public ExamRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Exams exam)
    {
        _db.Examination.Update(exam);
    }
}
