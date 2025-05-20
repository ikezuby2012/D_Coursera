using Application.Abstractions.Interface;
using Domain.Exams;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;

public class ExamAnswerRepository : Repository<ExamAnswer>, IExamAnswerRepository
{
    private readonly ApplicationDbContext _db;

    public ExamAnswerRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task AddRangeAsync(List<ExamAnswer> uploadedOptions, CancellationToken cancellationToken)
    {
        await _db.Set<ExamAnswer>().AddRangeAsync(uploadedOptions, cancellationToken);
    }

    public void Update(ExamAnswer examAnswer)
    {
        _db.ExamAnswers.Update(examAnswer);
    }

    public void UpdateRangeAsync(List<ExamAnswer> examAnswers, CancellationToken cancellationToken = default)
    {
        _db.Set<ExamAnswer>().UpdateRange(examAnswers);
    }
}
