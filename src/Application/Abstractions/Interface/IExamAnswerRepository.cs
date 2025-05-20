using Domain.Exams;

namespace Application.Abstractions.Interface;
public interface IExamAnswerRepository : IRepository<ExamAnswer>
{
    Task AddRangeAsync(List<ExamAnswer> uploadedOptions, CancellationToken cancellationToken);
    void Update(ExamAnswer examAnswer);
    void UpdateRangeAsync(List<ExamAnswer> examAnswers, CancellationToken cancellationToken = default);
}
