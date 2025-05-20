using Domain.Exams;

namespace Application.Abstractions.Interface;
public interface IExamSubmissionRepository : IRepository<ExamsSubmission>
{
    void Update(ExamsSubmission examSubmission);
}
