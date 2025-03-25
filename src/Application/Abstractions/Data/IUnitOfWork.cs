using Application.Abstractions.Interface;

namespace Application.Abstractions.Data;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ICourseRepository CourseRepository { get; }
    void Save();
    Task SaveAsync(CancellationToken cancellationToken);
    IMediaRepository MediaRepository { get; }
}
