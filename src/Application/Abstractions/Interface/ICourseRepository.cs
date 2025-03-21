using Domain.Course;

namespace Application.Abstractions.Interface;
public interface ICourseRepository : IRepository<Course>
{
    void Update(Course course);
}
