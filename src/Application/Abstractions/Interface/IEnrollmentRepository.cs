namespace Application.Abstractions.Interface;

public interface IEnrollmentRepository : IRepository<Domain.Enrollment.Enrollment>
{
    void Update(Domain.Enrollment.Enrollment enrollment);
}
