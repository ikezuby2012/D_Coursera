using Domain.Exams;

namespace Application.Abstractions.Interface;
public interface IExamQuestionsRepository : IRepository<ExamQuestions>
{
    Task AddRangeAsync(IEnumerable<ExamQuestions> examQuestions, CancellationToken cancellationToken);
    void Update(ExamQuestions examQuestion);
}
