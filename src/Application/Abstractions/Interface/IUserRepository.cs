using Domain.Users;

namespace Application.Abstractions.Interface;
public interface IUserRepository : IRepository<User>
{
    void Update(User user);
}
