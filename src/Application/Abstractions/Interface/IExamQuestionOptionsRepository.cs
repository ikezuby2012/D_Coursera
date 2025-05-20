using Domain.Exams;

namespace Application.Abstractions.Interface;

public interface IExamQuestionOptionsRepository : IRepository<ExamQuestionOption>
{
    Task AddRangeAsync(List<ExamQuestionOption> uploadedOptions, CancellationToken cancellationToken);
    void Update(ExamQuestionOption examQuestionOption);
    void UpdateRangeAsync(List<ExamQuestionOption> examOptions, CancellationToken cancellationToken = default);
}
