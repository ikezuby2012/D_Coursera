using Application.Abstractions.Interface;
using Domain.Exams;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;
public class ExamQuestionOptionRepository : Repository<ExamQuestionOption>, IExamQuestionOptionsRepository
{
    private readonly ApplicationDbContext _db;

    public ExamQuestionOptionRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task AddRangeAsync(List<ExamQuestionOption> uploadedOptions, CancellationToken cancellationToken)
    {
        if (uploadedOptions == null || !uploadedOptions.Any())
        {
            return;
        }

        await _db.ExamQuestionOptions.AddRangeAsync(uploadedOptions, cancellationToken);
    }

    public void Update(ExamQuestionOption examQuestionOption)
    {
        _db.ExamQuestionOptions.Update(examQuestionOption);
    }
}
