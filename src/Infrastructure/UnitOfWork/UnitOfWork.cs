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
    public IMediaRepository MediaRepository { get; init; }
    public IAssignmentRepository AssignmentRepository { get; init; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        UserRepository = new UserRepository(_db);
        CourseRepository = new CourseRepository(_db);
        MediaRepository = new MediaRepository(_db);
        AssignmentRepository = new AssignmentRepository(_db);
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
