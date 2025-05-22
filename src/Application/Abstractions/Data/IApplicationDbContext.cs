using Domain.Assignments;
using Domain.AssignmentSubmission;
using Domain.Course;
using Domain.Exams;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Course> Courses { get; }
    DbSet<Domain.Media.Media> Medias { get; }
    DbSet<Assignment> Assignments { get; }
    DbSet<AssignmentSubmissions> AssignmentSubmissions { get; }
    DbSet<Exams> Examination { get; }
    DbSet<ExamQuestions> ExamQuestions { get; }
    DbSet<ExamQuestionOption> ExamQuestionOptions { get; }
    DbSet<ExamsSubmission> ExamsSubmissions { get; }
    DbSet<ExamAnswer> ExamAnswers { get; }
    DbSet<Domain.Enrollment.Enrollment> Enrollment { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void GetAppLock(string appLockId);
    Task GetAppLockAsync(string appLockId, int timeOut, CancellationToken cancellationToken);
    DatabaseFacade DatabaseFacade { get; }
}
