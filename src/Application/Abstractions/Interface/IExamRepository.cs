using Domain.Exams;

namespace Application.Abstractions.Interface;
public interface IExamRepository : IRepository<Exams>
{
    void Update(Exams exam);
}
