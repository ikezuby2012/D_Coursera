using Application.Abstractions.Interface;
using Domain.Exams;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;

public class ExamSubmissionRepository : Repository<ExamsSubmission>, IExamSubmissionRepository
{
    private readonly ApplicationDbContext _db;

    public ExamSubmissionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(ExamsSubmission examSubmission)
    {
        _db.ExamsSubmissions.Update(examSubmission);
    }
}
