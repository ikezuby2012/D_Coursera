using Application.Abstractions.Interface;
using Domain.Exams;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;
public class ExamQuestionRepository : Repository<ExamQuestions>, IExamQuestionsRepository
{
    private readonly ApplicationDbContext _db;
    public ExamQuestionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task AddRangeAsync(IEnumerable<ExamQuestions> examQuestions, CancellationToken cancellationToken)
    {
        await _db.Set<ExamQuestions>().AddRangeAsync(examQuestions, cancellationToken);
    }

    public void Update(ExamQuestions examQuestion)
    {
        _db.ExamQuestions.Update(examQuestion);
    }
}
