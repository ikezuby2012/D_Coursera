using Application.Abstractions.Data;
using Application.Abstractions.Interface;
using Infrastructure.Database;
using Infrastructure.UnitOfWork.Repository;


namespace Infrastructure.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public IUserRepository UserRepository { get; init; }
    public ICourseRepository CourseRepository { get; init; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        UserRepository = new UserRepository(_db);
        CourseRepository = new CourseRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();

    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }
}
