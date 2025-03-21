using Application.Abstractions.Interface;
using Domain.Course;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;
public class CourseRepository : Repository<Course>, ICourseRepository
{
    private readonly ApplicationDbContext _db;

    public CourseRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Course course)
    {
        _db.Courses.Update(course);
    }
}
