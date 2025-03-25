using Domain.Course;
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
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DatabaseFacade DatabaseFacade { get; }
}
